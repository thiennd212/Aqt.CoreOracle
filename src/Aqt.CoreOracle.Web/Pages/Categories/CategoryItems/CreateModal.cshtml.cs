using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aqt.CoreOracle.Categories;
using Aqt.CoreOracle.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.Permissions;

namespace Aqt.CoreOracle.Web.Pages.Categories.CategoryItems;

[Authorize(CoreOraclePermissions.CategoryItems.Create)]
public class CreateModalModel : CoreOraclePageModel
{
    [BindProperty]
    public CreateUpdateCategoryItemDto CategoryItem { get; set; }

    public List<SelectListItem> CategoryTypes { get; set; }
    public List<SelectListItem> Parents { get; set; }

    private readonly ICategoryTypeAppService _categoryTypeAppService;
    private readonly ICategoryItemAppService _categoryItemAppService;

    public CreateModalModel(
        ICategoryTypeAppService categoryTypeAppService,
        ICategoryItemAppService categoryItemAppService)
    {
        _categoryTypeAppService = categoryTypeAppService;
        _categoryItemAppService = categoryItemAppService;
    }

    public async Task OnGetAsync()
    {
        CategoryItem = new CreateUpdateCategoryItemDto
        {
            IsActive = true,
            DisplayOrder = 0
        };

        await PopulateSelectListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _categoryItemAppService.CreateAsync(CategoryItem);
        return NoContent();
    }

    private async Task PopulateSelectListsAsync()
    {
        var categoryTypes = await _categoryTypeAppService.GetListAsync(new CategoryTypeGetListInput
        {
            MaxResultCount = 1000,
            IsActive = true
        });

        CategoryTypes = categoryTypes.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();

        Parents = new List<SelectListItem> { new(L["None"], "") };
    }
} 