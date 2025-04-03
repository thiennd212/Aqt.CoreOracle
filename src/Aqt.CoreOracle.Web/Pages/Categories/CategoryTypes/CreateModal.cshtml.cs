using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aqt.CoreOracle.Categories;
using Aqt.CoreOracle.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.Permissions;

namespace Aqt.CoreOracle.Web.Pages.Categories.CategoryTypes;

[Authorize(CoreOraclePermissions.CategoryTypes.Create)]
public class CreateModalModel : CoreOraclePageModel
{
    [BindProperty]
    public CreateUpdateCategoryTypeDto CategoryType { get; set; }

    private readonly ICategoryTypeAppService _categoryTypeAppService;

    public CreateModalModel(ICategoryTypeAppService categoryTypeAppService)
    {
        _categoryTypeAppService = categoryTypeAppService;
    }

    public void OnGet()
    {
        CategoryType = new CreateUpdateCategoryTypeDto
        {
            IsActive = true
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _categoryTypeAppService.CreateAsync(CategoryType);
        return NoContent();
    }
} 