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

[Authorize(CoreOraclePermissions.CategoryTypes.Default)]
public class CategoryTypeAppService : CoreOracleAppService, ICategoryTypeAppService
{
    private readonly ICategoryTypeRepository _categoryTypeRepository;

    public CategoryTypeAppService(ICategoryTypeRepository categoryTypeRepository)
    {
        _categoryTypeRepository = categoryTypeRepository;
    }

    public async Task<CategoryTypeDto> GetAsync(Guid id)
    {
        var categoryType = await _categoryTypeRepository.GetAsync(id);
        return ObjectMapper.Map<CategoryType, CategoryTypeDto>(categoryType);
    }

    public async Task<CategoryTypeDto> GetByCodeAsync(string code)
    {
        var categoryType = await _categoryTypeRepository.GetByCodeAsync(code);
        return ObjectMapper.Map<CategoryType, CategoryTypeDto>(categoryType);
    }

    public async Task<PagedResultDto<CategoryTypeDto>> GetListAsync(CategoryTypeGetListInput input)
    {
        var query = await _categoryTypeRepository.GetQueryableAsync();
        query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                x => (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(input.Filter)) || 
                     (!string.IsNullOrEmpty(x.Code) && x.Code.Contains(input.Filter)))
            .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);

        var totalCount = await AsyncExecuter.CountAsync(query);
        
        query = query.OrderBy(input.Sorting ?? nameof(CategoryType.Name))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<CategoryTypeDto>(
            totalCount,
            ObjectMapper.Map<List<CategoryType>, List<CategoryTypeDto>>(items)
        );
    }

    [Authorize(CoreOraclePermissions.CategoryTypes.Create)]
    public async Task<CategoryTypeDto> CreateAsync(CreateUpdateCategoryTypeDto input)
    {
        await ValidateCodeUniquenessAsync(input.Code);

        var categoryType = new CategoryType(
            GuidGenerator.Create(),
            input.Code,
            input.Name,
            input.Description,
            input.IsActive,
            input.AllowMultipleSelect
        );

        await _categoryTypeRepository.InsertAsync(categoryType);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<CategoryType, CategoryTypeDto>(categoryType);
    }

    [Authorize(CoreOraclePermissions.CategoryTypes.Edit)]
    public async Task<CategoryTypeDto> UpdateAsync(Guid id, CreateUpdateCategoryTypeDto input)
    {
        var categoryType = await _categoryTypeRepository.GetAsync(id);
        if (categoryType == null)
        {
            throw new EntityNotFoundException(typeof(CategoryType), id);
        }

        if (categoryType.Code != input.Code)
        {
            await ValidateCodeUniquenessAsync(input.Code);
            categoryType.SetCode(input.Code);
        }

        categoryType.SetName(input.Name);
        categoryType.Description = input.Description;
        categoryType.SetActive(input.IsActive);
        categoryType.AllowMultipleSelect = input.AllowMultipleSelect;

        await _categoryTypeRepository.UpdateAsync(categoryType);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<CategoryType, CategoryTypeDto>(categoryType);
    }

    [Authorize(CoreOraclePermissions.CategoryTypes.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _categoryTypeRepository.DeleteAsync(id);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    private async Task ValidateCodeUniquenessAsync(string code)
    {
        var exists = await _categoryTypeRepository.AnyAsync(x => x.Code == code);
        if (exists)
        {
            throw new CategoryCodeAlreadyExistsException(code);
        }
    }
} 