using System;
using Shouldly;
using Xunit;

namespace Aqt.CoreOracle.Categories;

public class CategoryTypeTests : CoreOracleDomainTestBase<CoreOracleDomainTestModule>
{
    [Fact]
    public void Should_Create_Valid_CategoryType()
    {
        // Arrange
        var id = Guid.NewGuid();
        var code = "TEST";
        var name = "Test Category";
        var description = "Test Description";

        // Act
        var categoryType = new CategoryType(id, code, name)
        {
            Description = description
        };

        // Assert
        categoryType.Id.ShouldBe(id);
        categoryType.Code.ShouldBe(code);
        categoryType.Name.ShouldBe(name);
        categoryType.Description.ShouldBe(description);
        categoryType.IsActive.ShouldBeTrue(); // Default value
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Not_Create_With_Invalid_Code(string code)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            new CategoryType(Guid.NewGuid(), code, "Test");
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
            new CategoryType(Guid.NewGuid(), "TEST", name);
        });

        exception.ParamName.ShouldBe("name");
    }

    [Fact]
    public void Should_Change_Active_Status()
    {
        // Arrange
        var categoryType = new CategoryType(Guid.NewGuid(), "TEST", "Test Category");
        
        // Act & Assert
        categoryType.IsActive.ShouldBeTrue(); // Default value
        
        categoryType.SetActive(false);
        categoryType.IsActive.ShouldBeFalse();
        
        categoryType.SetActive(true);
        categoryType.IsActive.ShouldBeTrue();
    }

    [Fact]
    public void Should_Update_Name()
    {
        // Arrange
        var categoryType = new CategoryType(Guid.NewGuid(), "TEST", "Test Category");
        var newName = "Updated Category";

        // Act
        categoryType.SetName(newName);

        // Assert
        categoryType.Name.ShouldBe(newName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Not_Update_With_Invalid_Name(string newName)
    {
        // Arrange
        var categoryType = new CategoryType(Guid.NewGuid(), "TEST", "Test Category");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            categoryType.SetName(newName);
        });

        exception.ParamName.ShouldBe("name");
    }
} 