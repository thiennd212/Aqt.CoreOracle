using System;
using Shouldly;
using Xunit;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemTests : CoreOracleDomainTestBase<CoreOracleDomainTestModule>
{
    [Fact]
    public void Should_Create_Valid_CategoryItem()
    {
        // Arrange
        var id = Guid.NewGuid();
        var categoryTypeId = Guid.NewGuid();
        var code = "TEST";
        var name = "Test Item";
        var displayOrder = 1;
        var value = "Test Value";
        var icon = "test-icon";
        var extraProperties = "{\"key\":\"value\"}";

        // Act
        var categoryItem = new CategoryItem(
            id,
            categoryTypeId,
            code,
            name,
            description: value,
            isActive: true,
            icon: icon,
            extraProperties: extraProperties);

        // Assert
        categoryItem.Id.ShouldBe(id);
        categoryItem.CategoryTypeId.ShouldBe(categoryTypeId);
        categoryItem.Code.ShouldBe(code);
        categoryItem.Name.ShouldBe(name);
        categoryItem.DisplayOrder.ShouldBe(displayOrder);
        categoryItem.ParentId.ShouldBeNull();
        categoryItem.IsActive.ShouldBeTrue();
        categoryItem.Value.ShouldBe(value);
        categoryItem.Icon.ShouldBe(icon);
        categoryItem.ExtraProperties.ShouldBe(extraProperties);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Not_Create_With_Invalid_Code(string code)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            new CategoryItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                code,
                "Test");
        });

        exception.ParamName.ShouldBe("code");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Not_Create_With_Invalid_Name(string name)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            new CategoryItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "TEST",
                name);
        });

        exception.ParamName.ShouldBe("name");
    }

    [Fact]
    public void Should_Create_With_Parent()
    {
        // Arrange
        var parentId = Guid.NewGuid();

        // Act
        var categoryItem = new CategoryItem(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "TEST",
            "Test Item",
            parentId: parentId);

        // Assert
        categoryItem.ParentId.ShouldBe(parentId);
    }

    [Fact]
    public void Should_Create_With_Default_Values()
    {
        // Act
        var categoryItem = new CategoryItem(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "TEST",
            "Test Item");

        // Assert
        categoryItem.DisplayOrder.ShouldBe(0);
        categoryItem.ParentId.ShouldBeNull();
        categoryItem.IsActive.ShouldBeTrue();
        categoryItem.Value.ShouldBe("");
        categoryItem.Icon.ShouldBe("");
        categoryItem.ExtraProperties.ShouldBe("");
    }
} 