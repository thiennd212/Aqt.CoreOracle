using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Countries;
using Aqt.CoreOracle.Application.Contracts.Provinces;
using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Aqt.CoreOracle.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aqt.CoreOracle.Web.Pages.Provinces;

public class EditModalModel : CoreOraclePageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateProvinceDto Province { get; set; } = new();

    public List<SelectListItem> CountryLookup { get; set; } = new();

    private readonly IProvinceAppService _provinceAppService;
    private readonly ICountryAppService _countryAppService;

    public EditModalModel(
        IProvinceAppService provinceAppService,
        ICountryAppService countryAppService)
    {
        _provinceAppService = provinceAppService;
        _countryAppService = countryAppService;
    }

    public virtual async Task OnGetAsync()
    {
        var provinceDto = await _provinceAppService.GetAsync(Id);
        Province = ObjectMapper.Map<ProvinceDto, CreateUpdateProvinceDto>(provinceDto);

        var countryLookupDto = await _countryAppService.GetLookupAsync(); // Potential Error: Needs checking
        CountryLookup = countryLookupDto.Items
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _provinceAppService.UpdateAsync(Id, Province);
        return NoContent();
    }
} 