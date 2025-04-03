using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Aqt.CoreOracle.Categories.Dtos;
using Volo.Abp.Domain.Entities;
using System.Collections.Generic;

namespace Aqt.CoreOracle.Categories;

public class CategoryTypeAppServiceTests : CoreOracleApplicationTestBase<CoreOracleApplicationTestModule>
{
    private readonly ICategoryTypeAppService _categoryTypeAppService;
    private readonly ICategoryTypeRepository _categoryTypeRepository;

    public CategoryTypeAppServiceTests()
    {
        _categoryTypeAppService = GetRequiredService<ICategoryTypeAppService>();
        _categoryTypeRepository = GetRequiredService<ICategoryTypeRepository>();
    }

    [Fact]
    public async Task Should_Get_Category_Type()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST",
                "Test Category",
                "Test Description"
            )
        );

        // Act
        var result = await _categoryTypeAppService.GetAsync(categoryType.Id);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryType.Id);
        result.Code.ShouldBe(categoryType.Code);
        result.Name.ShouldBe(categoryType.Name);
        result.Description.ShouldBe(categoryType.Description);
    }

    [Fact]
    public async Task Should_Get_Category_Type_By_Code()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST",
                "Test Category"
            )
        );

        // Act
        var result = await _categoryTypeAppService.GetByCodeAsync(categoryType.Code);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryType.Id);
        result.Code.ShouldBe(categoryType.Code);
        result.Name.ShouldBe(categoryType.Name);
    }

    [Fact]
    public async Task Should_Get_List()
    {
        // Arrange
        await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST1",
                "Test Category 1"
            )
        );

        await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST2",
                "Test Category 2"
            )
        );

        // Act
        var result = await _categoryTypeAppService.GetListAsync(
            new CategoryTypeGetListInput
            {
                MaxResultCount = 10,
                SkipCount = 0,
                Sorting = "Code"
            });

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count.ShouldBeGreaterThanOrEqualTo(2);
        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task Should_Create_Category_Type()
    {
        // Arrange
        var input = new CreateUpdateCategoryTypeDto
        {
            Code = "NEW",
            Name = "New Category",
            Description = "New Description",
            IsActive = true,
            AllowMultipleSelect = true
        };

        // Act
        var result = await _categoryTypeAppService.CreateAsync(input);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.Code.ShouldBe(input.Code);
        result.Name.ShouldBe(input.Name);
        result.Description.ShouldBe(input.Description);
        result.IsActive.ShouldBe(input.IsActive);
        result.AllowMultipleSelect.ShouldBe(input.AllowMultipleSelect);

        // Verify from repository
        var categoryType = await _categoryTypeRepository.GetAsync(result.Id);
        categoryType.ShouldNotBeNull();
        categoryType.Code.ShouldBe(input.Code);
        categoryType.Name.ShouldBe(input.Name);
    }

    [Fact]
    public async Task Should_Update_Category_Type()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST",
                "Test Category"
            )
        );

        var input = new CreateUpdateCategoryTypeDto
        {
            Code = "UPDATED",
            Name = "Updated Category",
            Description = "Updated Description",
            IsActive = false,
            AllowMultipleSelect = true
        };

        // Act
        var result = await _categoryTypeAppService.UpdateAsync(categoryType.Id, input);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryType.Id);
        result.Code.ShouldBe(input.Code);
        result.Name.ShouldBe(input.Name);
        result.Description.ShouldBe(input.Description);
        result.IsActive.ShouldBe(input.IsActive);
        result.AllowMultipleSelect.ShouldBe(input.AllowMultipleSelect);

        // Verify from repository
        var updatedCategoryType = await _categoryTypeRepository.GetAsync(categoryType.Id);
        updatedCategoryType.ShouldNotBeNull();
        updatedCategoryType.Code.ShouldBe(input.Code);
        updatedCategoryType.Name.ShouldBe(input.Name);
    }

    [Fact]
    public async Task Should_Delete_Category_Type()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST",
                "Test Category"
            )
        );

        // Act
        await _categoryTypeAppService.DeleteAsync(categoryType.Id);

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _categoryTypeRepository.GetAsync(categoryType.Id);
        });
    }

    [Fact]
    public async Task Should_Not_Allow_Duplicate_Code()
    {
        // Arrange
        await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TEST",
                "Test Category"
            )
        );

        var input = new CreateUpdateCategoryTypeDto
        {
            Code = "TEST", // Same code
            Name = "Another Category"
        };

        // Act & Assert
        await Assert.ThrowsAsync<CategoryCodeAlreadyExistsException>(async () =>
        {
            await _categoryTypeAppService.CreateAsync(input);
        });
    }
} 