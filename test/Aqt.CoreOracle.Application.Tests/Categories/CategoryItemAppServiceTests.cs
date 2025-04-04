using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Aqt.CoreOracle.Categories.Dtos;
using Volo.Abp.Domain.Entities;
using System.Collections.Generic;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemAppServiceTests : CoreOracleApplicationTestBase<CoreOracleApplicationTestModule>
{
    private readonly ICategoryItemAppService _categoryItemAppService;
    private readonly ICategoryItemRepository _categoryItemRepository;
    private readonly ICategoryTypeRepository _categoryTypeRepository;

    public CategoryItemAppServiceTests()
    {
        _categoryItemAppService = GetRequiredService<ICategoryItemAppService>();
        _categoryItemRepository = GetRequiredService<ICategoryItemRepository>();
        _categoryTypeRepository = GetRequiredService<ICategoryTypeRepository>();
    }

    [Fact]
    public async Task Should_Get_Category_Item()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var categoryItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST",
                "Test Item",
                value: "Test Value"
            )
        );

        // Act
        var result = await _categoryItemAppService.GetAsync(categoryItem.Id);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryItem.Id);
        result.CategoryTypeId.ShouldBe(categoryItem.CategoryTypeId);
        result.Code.ShouldBe(categoryItem.Code);
        result.Name.ShouldBe(categoryItem.Name);
        result.Value.ShouldBe(categoryItem.Value);
    }

    [Fact]
    public async Task Should_Get_Category_Item_By_Code()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var categoryItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST",
                "Test Item"
            )
        );

        // Act
        var result = await _categoryItemAppService.GetByCodeAsync(categoryType.Id, categoryItem.Code);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryItem.Id);
        result.Code.ShouldBe(categoryItem.Code);
        result.Name.ShouldBe(categoryItem.Name);
    }

    [Fact]
    public async Task Should_Get_List()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST1",
                "Test Item 1"
            )
        );

        await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST2",
                "Test Item 2"
            )
        );

        // Act
        var result = await _categoryItemAppService.GetListAsync(
            new CategoryItemGetListInput
            {
                CategoryTypeId = categoryType.Id,
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
    public async Task Should_Get_List_By_Type_Code()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST1",
                "Test Item 1"
            )
        );

        await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST2",
                "Test Item 2"
            )
        );

        // Act
        var result = await _categoryItemAppService.GetListByTypeCodeAsync(categoryType.Code);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task Should_Create_Category_Item()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var input = new CreateCategoryItemDto
        {
            CategoryTypeId = categoryType.Id,
            Code = "NEW",
            Name = "New Item",
            DisplayOrder = 1,
            IsActive = true,
            Value = "Test Value",
            Icon = "test-icon",
            ExtraProperties = "{\"key\":\"value\"}"
        };

        // Act
        var result = await _categoryItemAppService.CreateAsync(input);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.CategoryTypeId.ShouldBe(input.CategoryTypeId);
        result.Code.ShouldBe(input.Code);
        result.Name.ShouldBe(input.Name);
        result.DisplayOrder.ShouldBe(input.DisplayOrder);
        result.IsActive.ShouldBe(input.IsActive);
        result.Value.ShouldBe(input.Value);
        result.Icon.ShouldBe(input.Icon);
        result.ExtraProperties.ShouldBe(input.ExtraProperties);

        // Verify from repository
        var categoryItem = await _categoryItemRepository.GetAsync(result.Id);
        categoryItem.ShouldNotBeNull();
        categoryItem.Code.ShouldBe(input.Code);
        categoryItem.Name.ShouldBe(input.Name);
    }

    [Fact]
    public async Task Should_Create_Category_Item_With_Parent()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var parent = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "PARENT",
                "Parent Item"
            )
        );

        var input = new CreateCategoryItemDto
        {
            CategoryTypeId = categoryType.Id,
            Code = "CHILD",
            Name = "Child Item",
            ParentId = parent.Id
        };

        // Act
        var result = await _categoryItemAppService.CreateAsync(input);

        // Assert
        result.ShouldNotBeNull();
        result.ParentId.ShouldBe(parent.Id);

        // Verify from repository
        var categoryItem = await _categoryItemRepository.GetAsync(result.Id);
        categoryItem.ShouldNotBeNull();
        categoryItem.ParentId.ShouldBe(parent.Id);
    }

    [Fact]
    public async Task Should_Update_Category_Item()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var categoryItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST",
                "Test Item"
            )
        );

        var input = new UpdateCategoryItemDto
        {
            CategoryTypeId = categoryType.Id,
            Code = "UPDATED",
            Name = "Updated Item",
            DisplayOrder = 2,
            IsActive = false,
            Value = "Updated Value",
            Icon = "updated-icon",
            ExtraProperties = "{\"key\":\"updated\"}"
        };

        // Act
        var result = await _categoryItemAppService.UpdateAsync(categoryItem.Id, input);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(categoryItem.Id);
        result.CategoryTypeId.ShouldBe(input.CategoryTypeId);
        result.Code.ShouldBe(input.Code);
        result.Name.ShouldBe(input.Name);
        result.DisplayOrder.ShouldBe(input.DisplayOrder);
        result.IsActive.ShouldBe(input.IsActive);
        result.Value.ShouldBe(input.Value);
        result.Icon.ShouldBe(input.Icon);
        result.ExtraProperties.ShouldBe(input.ExtraProperties);

        // Verify from repository
        var updatedItem = await _categoryItemRepository.GetAsync(categoryItem.Id);
        updatedItem.ShouldNotBeNull();
        updatedItem.Code.ShouldBe(input.Code);
        updatedItem.Name.ShouldBe(input.Name);
    }

    [Fact]
    public async Task Should_Delete_Category_Item()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE",
                "Test Type"
            )
        );

        var categoryItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST",
                "Test Item"
            )
        );

        // Act
        await _categoryItemAppService.DeleteAsync(categoryItem.Id);

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _categoryItemRepository.GetAsync(categoryItem.Id);
        });
    }

    [Fact]
    public async Task Should_Not_Allow_Duplicate_Code()
    {
        // Arrange
        var categoryType = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE1",
                "Test Type 1"
            )
        );

        var existingItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "TEST",
                "Test Item"
            )
        );

        var createInput = new CreateCategoryItemDto
        {
            CategoryTypeId = categoryType.Id,
            Code = "TEST",
            Name = "Another Test Item"
        };

        // Act & Assert
        await Assert.ThrowsAsync<CategoryCodeAlreadyExistsException>(async () =>
        {
            await _categoryItemAppService.CreateAsync(createInput);
        });

        // Should also throw when updating another item with same code
        var anotherItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType.Id,
                "ANOTHER",
                "Another Item"
            )
        );

        var updateInput = new UpdateCategoryItemDto
        {
            CategoryTypeId = categoryType.Id,
            Code = "TEST",
            Name = "Another Test Item"
        };

        await Assert.ThrowsAsync<CategoryCodeAlreadyExistsException>(async () =>
        {
            await _categoryItemAppService.UpdateAsync(anotherItem.Id, updateInput);
        });
    }

    [Fact]
    public async Task Should_Not_Allow_Parent_From_Different_Type()
    {
        // Arrange
        var categoryType1 = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE1",
                "Test Type 1"
            )
        );

        var categoryType2 = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE2",
                "Test Type 2"
            )
        );

        var parent = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType1.Id,
                "PARENT",
                "Parent Item"
            )
        );

        var input = new CreateCategoryItemDto
        {
            CategoryTypeId = categoryType2.Id,
            Code = "CHILD",
            Name = "Child Item",
            ParentId = parent.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<CategoryItemParentTypeMismatchException>(async () =>
        {
            await _categoryItemAppService.CreateAsync(input);
        });
    }

    [Fact]
    public async Task Should_Not_Allow_Type_Change()
    {
        // Arrange
        var categoryType1 = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE1",
                "Test Type 1"
            )
        );

        var categoryType2 = await _categoryTypeRepository.InsertAsync(
            new CategoryType(
                Guid.NewGuid(),
                "TYPE2",
                "Test Type 2"
            )
        );

        var categoryItem = await _categoryItemRepository.InsertAsync(
            new CategoryItem(
                Guid.NewGuid(),
                categoryType1.Id,
                "TEST",
                "Test Item"
            )
        );

        var input = new UpdateCategoryItemDto
        {
            CategoryTypeId = categoryType2.Id,
            Code = "TEST",
            Name = "Test Item"
        };

        // Act & Assert
        await Assert.ThrowsAsync<CategoryItemTypeMismatchException>(async () =>
        {
            await _categoryItemAppService.UpdateAsync(categoryItem.Id, input);
        });
    }
} 