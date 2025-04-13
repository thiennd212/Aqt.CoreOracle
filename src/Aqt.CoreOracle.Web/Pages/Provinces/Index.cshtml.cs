using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Countries;
using Aqt.CoreOracle.Web.Pages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aqt.CoreOracle.Web.Pages.Provinces;

public class IndexModel : CoreOraclePageModel
{
    public List<SelectListItem> CountryLookup { get; set; } = new();

    private readonly ICountryAppService _countryAppService;

    public IndexModel(ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
    }

    public async Task OnGetAsync()
    {
        var countryLookupDto = await _countryAppService.GetLookupAsync();

        if (countryLookupDto?.Items != null) 
        {
             CountryLookup = countryLookupDto.Items
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();
        }
        else
        {
            CountryLookup = new List<SelectListItem>(); 
        }
        
        CountryLookup.Insert(0, new SelectListItem(L["All"], ""));
    }
} 