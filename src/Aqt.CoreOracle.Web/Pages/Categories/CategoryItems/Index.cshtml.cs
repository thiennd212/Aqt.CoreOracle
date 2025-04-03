using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.Categories;
using Aqt.CoreOracle.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aqt.CoreOracle.Permissions;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Aqt.CoreOracle.Web.Pages.Categories.CategoryItems;

[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public class IndexModel : CoreOraclePageModel
{
    private readonly ICategoryTypeAppService _categoryTypeAppService;
    private readonly ICategoryItemAppService _categoryItemAppService;

    public IndexModel(
        ICategoryTypeAppService categoryTypeAppService,
        ICategoryItemAppService categoryItemAppService)
    {
        _categoryTypeAppService = categoryTypeAppService;
        _categoryItemAppService = categoryItemAppService;
    }

    public List<SelectListItem> CategoryTypes { get; set; }
    public List<SelectListItem> Parents { get; set; }
    public List<SelectListItem> Statuses { get; set; }

    public Guid? CategoryTypeId { get; set; }
    public Guid? ParentId { get; set; }
    public string Filter { get; set; }
    public bool? IsActive { get; set; }

    public async Task OnGetAsync()
    {
        await PopulateSelectListsAsync();
    }

    private async Task PopulateSelectListsAsync()
    {
        // Get category types
        var categoryTypes = await _categoryTypeAppService.GetListAsync(new CategoryTypeGetListInput
        {
            MaxResultCount = 1000,
            IsActive = true
        });

        CategoryTypes = categoryTypes.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .Prepend(new SelectListItem(L["All"], ""))
            .ToList();

        // Get parent items if category type is selected
        Parents = new List<SelectListItem> { new(L["All"], "") };
        if (CategoryTypeId.HasValue)
        {
            var parents = await _categoryItemAppService.GetListAsync(new CategoryItemGetListInput
            {
                CategoryTypeId = CategoryTypeId.Value,
                MaxResultCount = 1000,
                IsActive = true
            });

            Parents.AddRange(parents.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())));
        }

        // Status list
        Statuses = new List<SelectListItem>
        {
            new(L["All"], ""),
            new(L["Active"], "true"),
            new(L["Inactive"], "false")
        };
    }
} 