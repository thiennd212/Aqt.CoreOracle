using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aqt.CoreOracle.Categories;
using Aqt.CoreOracle.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.Permissions;

namespace Aqt.CoreOracle.Web.Pages.Categories.CategoryTypes;

[Authorize(CoreOraclePermissions.CategoryTypes.Edit)]
public class EditModalModel : CoreOraclePageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateCategoryTypeDto CategoryType { get; set; }

    private readonly ICategoryTypeAppService _categoryTypeAppService;

    public EditModalModel(ICategoryTypeAppService categoryTypeAppService)
    {
        _categoryTypeAppService = categoryTypeAppService;
    }

    public async Task OnGetAsync()
    {
        var categoryType = await _categoryTypeAppService.GetAsync(Id);
        CategoryType = ObjectMapper.Map<CategoryTypeDto, CreateUpdateCategoryTypeDto>(categoryType);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _categoryTypeAppService.UpdateAsync(Id, CategoryType);
        return NoContent();
    }
} 