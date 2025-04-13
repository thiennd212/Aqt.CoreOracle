using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Countries;
using Aqt.CoreOracle.Application.Contracts.Provinces;
using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Aqt.CoreOracle.Web.Pages;
using Aqt.CoreOracle.Web.Pages.Provinces.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aqt.CoreOracle.Web.Pages.Provinces;

public class CreateModalModel : CoreOraclePageModel
{
    [BindProperty]
    public CreateProvinceViewModel ProvinceViewModel { get; set; }

    public List<SelectListItem> CountryLookup { get; set; } = new();

    private readonly IProvinceAppService _provinceAppService;
    private readonly ICountryAppService _countryAppService;

    public CreateModalModel(
        IProvinceAppService provinceAppService,
        ICountryAppService countryAppService)
    {
        _provinceAppService = provinceAppService;
        _countryAppService = countryAppService;
    }

    public virtual async Task OnGetAsync()
    {
        ProvinceViewModel = new CreateProvinceViewModel();

        var countryLookupDto = await _countryAppService.GetLookupAsync();
        CountryLookup = countryLookupDto.Items
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var provinceDto = ObjectMapper.Map<CreateProvinceViewModel, CreateUpdateProvinceDto>(ProvinceViewModel);
        
        await _provinceAppService.CreateAsync(provinceDto);
        return NoContent();
    }
} 