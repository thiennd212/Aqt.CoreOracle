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

[Authorize(CoreOraclePermissions.CategoryItems.Edit)]
public class EditModalModel : CoreOraclePageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public UpdateCategoryItemDto CategoryItem { get; set; }

    public List<SelectListItem> CategoryTypes { get; set; }
    public List<SelectListItem> Parents { get; set; }

    private readonly ICategoryTypeAppService _categoryTypeAppService;
    private readonly ICategoryItemAppService _categoryItemAppService;

    public EditModalModel(
        ICategoryTypeAppService categoryTypeAppService,
        ICategoryItemAppService categoryItemAppService)
    {
        _categoryTypeAppService = categoryTypeAppService;
        _categoryItemAppService = categoryItemAppService;
    }

    public async Task OnGetAsync()
    {
        var categoryItem = await _categoryItemAppService.GetAsync(Id);
        CategoryItem = ObjectMapper.Map<CategoryItemDto, UpdateCategoryItemDto>(categoryItem);

        await PopulateSelectListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _categoryItemAppService.UpdateAsync(Id, CategoryItem);
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

        var items = await _categoryItemAppService.GetListAsync(new CategoryItemGetListInput
        {
            MaxResultCount = 1000,
            CategoryTypeId = CategoryItem.CategoryTypeId
        });

        Parents = items.Items
            .Where(x => x.Id != Id)
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }
} 