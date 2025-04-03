using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Aqt.CoreOracle.Categories.Dtos;
using Aqt.CoreOracle.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Domain.Entities;

namespace Aqt.CoreOracle.Categories;

/// <summary>
/// Provides application service methods for managing CategoryItems.
/// Implements business logic and validation rules for CRUD operations.
/// </summary>
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public class CategoryItemAppService : CoreOracleAppService, ICategoryItemAppService
{
    private readonly ICategoryItemRepository _categoryItemRepository;
    private readonly ICategoryTypeRepository _categoryTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryItemAppService"/> class.
    /// </summary>
    /// <param name="categoryItemRepository">The repository for CategoryItem entities.</param>
    /// <param name="categoryTypeRepository">The repository for CategoryType entities.</param>
    public CategoryItemAppService(
        ICategoryItemRepository categoryItemRepository,
        ICategoryTypeRepository categoryTypeRepository)
    {
        _categoryItemRepository = categoryItemRepository;
        _categoryTypeRepository = categoryTypeRepository;
    }

    /// <summary>
    /// Gets a list of CategoryItems with optional filtering, sorting and paging.
    /// </summary>
    /// <param name="input">The input parameters for filtering and paging.</param>
    /// <returns>A paged list of CategoryItemDto objects.</returns>
    public async Task<PagedResultDto<CategoryItemDto>> GetListAsync(CategoryItemGetListInput input)
    {
        var queryable = await _categoryItemRepository.GetQueryableAsync();

        queryable = queryable
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x =>
                (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(input.Filter)) ||
                (!string.IsNullOrEmpty(x.Code) && x.Code.Contains(input.Filter)) ||
                (!string.IsNullOrEmpty(x.Description) && x.Description.Contains(input.Filter)))
            .WhereIf(input.CategoryTypeId != Guid.Empty, x => x.CategoryTypeId == input.CategoryTypeId);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable
            .OrderBy(input.Sorting ?? nameof(CategoryItem.Code))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(queryable);

        return new PagedResultDto<CategoryItemDto>(
            totalCount,
            ObjectMapper.Map<List<CategoryItem>, List<CategoryItemDto>>(items)
        );
    }

    /// <summary>
    /// Gets a CategoryItem by its ID.
    /// </summary>
    /// <param name="id">The ID of the CategoryItem to retrieve.</param>
    /// <returns>The CategoryItemDto if found.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the CategoryItem is not found.</exception>
    public async Task<CategoryItemDto> GetAsync(Guid id)
    {
        var categoryItem = await _categoryItemRepository.GetAsync(id);
        return ObjectMapper.Map<CategoryItem, CategoryItemDto>(categoryItem);
    }

    /// <summary>
    /// Creates a new CategoryItem.
    /// </summary>
    /// <param name="input">The data for creating the new CategoryItem.</param>
    /// <returns>The created CategoryItemDto.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when:
    /// - The CategoryType does not exist
    /// - The Code already exists in the same CategoryType
    /// - The Parent CategoryItem does not exist or is of a different type
    /// </exception>
    [Authorize(CoreOraclePermissions.CategoryItems.Create)]
    public async Task<CategoryItemDto> CreateAsync(CreateUpdateCategoryItemDto input)
    {
        await ValidateCodeUniquenessAsync(input.CategoryTypeId, input.Code);

        var categoryItem = new CategoryItem(
            GuidGenerator.Create(),
            input.CategoryTypeId,
            input.Code,
            input.Name,
            description: input.Description,
            isActive: input.IsActive
        );

        await _categoryItemRepository.InsertAsync(categoryItem);

        return ObjectMapper.Map<CategoryItem, CategoryItemDto>(categoryItem);
    }

    /// <summary>
    /// Updates an existing CategoryItem.
    /// </summary>
    /// <param name="id">The ID of the CategoryItem to update.</param>
    /// <param name="input">The new data for the CategoryItem.</param>
    /// <returns>The updated CategoryItemDto.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the CategoryItem is not found.</exception>
    /// <exception cref="BusinessException">
    /// Thrown when:
    /// - The Code already exists in the same CategoryType
    /// - The Parent CategoryItem does not exist or is of a different type
    /// - Attempting to change the CategoryType
    /// </exception>
    [Authorize(CoreOraclePermissions.CategoryItems.Edit)]
    public async Task<CategoryItemDto> UpdateAsync(Guid id, CreateUpdateCategoryItemDto input)
    {
        var categoryItem = await _categoryItemRepository.GetAsync(id);

        if (categoryItem.Code != input.Code)
        {
            await ValidateCodeUniquenessAsync(input.CategoryTypeId, input.Code);
        }

        categoryItem.CategoryTypeId = input.CategoryTypeId;
        categoryItem.Code = input.Code;
        categoryItem.Name = input.Name;
        categoryItem.Description = input.Description;
        categoryItem.IsActive = input.IsActive;

        await _categoryItemRepository.UpdateAsync(categoryItem);

        return ObjectMapper.Map<CategoryItem, CategoryItemDto>(categoryItem);
    }

    /// <summary>
    /// Deletes a CategoryItem.
    /// </summary>
    /// <param name="id">The ID of the CategoryItem to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the CategoryItem is not found.</exception>
    /// <exception cref="BusinessException">
    /// Thrown when:
    /// - The CategoryItem has children
    /// - The CategoryItem is in use by other entities
    /// </exception>
    [Authorize(CoreOraclePermissions.CategoryItems.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _categoryItemRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Checks if a Code is available within a CategoryType.
    /// </summary>
    /// <param name="input">The input containing CategoryTypeId and Code to check.</param>
    /// <returns>True if the Code is available, false otherwise.</returns>
    public async Task<bool> IsCodeExistAsync(Guid categoryTypeId, string code, Guid? expectedId = null)
    {
        return await _categoryItemRepository.IsCodeExistAsync(categoryTypeId, code, expectedId);
    }

    private async Task ValidateCodeUniquenessAsync(Guid categoryTypeId, string code)
    {
        if (await IsCodeExistAsync(categoryTypeId, code))
        {
            throw new CategoryItemCodeAlreadyExistsException(code);
        }
    }

    public async Task<CategoryItemDto> GetByCodeAsync(Guid categoryTypeId, string code)
    {
        var categoryItem = await _categoryItemRepository.GetByCodeAsync(categoryTypeId, code);
        return ObjectMapper.Map<CategoryItem, CategoryItemDto>(categoryItem);
    }

    public async Task<List<CategoryItemDto>> GetListByTypeCodeAsync(string categoryTypeCode)
    {
        var categoryType = await _categoryTypeRepository.GetByCodeAsync(categoryTypeCode);
        var items = await _categoryItemRepository.GetListAsync(categoryTypeId: categoryType.Id);
        return ObjectMapper.Map<List<CategoryItem>, List<CategoryItemDto>>(items);
    }

    public async Task<bool> CheckCodeAvailabilityAsync(CheckCategoryItemCodeAvailabilityInput input)
    {
        return !await IsCodeExistAsync(input.CategoryTypeId, input.Code, input.ExpectedId);
    }

    public async Task<List<CategoryItemLookupDto>> GetLookupAsync(Guid? categoryTypeId = null)
    {
        var items = await _categoryItemRepository.GetListAsync(categoryTypeId: categoryTypeId);
        return ObjectMapper.Map<List<CategoryItem>, List<CategoryItemLookupDto>>(items);
    }
} 