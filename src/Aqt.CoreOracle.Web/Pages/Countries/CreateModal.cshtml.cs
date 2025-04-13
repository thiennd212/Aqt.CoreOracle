using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aqt.CoreOracle.Application.Contracts.Countries; // Namespace for ICountryAppService
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos; // Namespace for CreateUpdateCountryDto
using Aqt.CoreOracle.Web.Pages; // Namespace for CoreOraclePageModel

namespace Aqt.CoreOracle.Web.Pages.Countries;

public class CreateModalModel : CoreOraclePageModel
{
    [BindProperty]
    public CreateUpdateCountryDto? Country { get; set; }

    private readonly ICountryAppService _countryAppService;

    public CreateModalModel(ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
        // ObjectMapper is inherited from CoreOraclePageModel / AbpPageModel
    }

    public virtual void OnGet()
    {
        Country = new CreateUpdateCountryDto(); // Initialize the DTO for the form
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _countryAppService.CreateAsync(Country!); // Used null-forgiving as it's initialized in OnGet
        return NoContent(); // Indicate success with no content
    }
} 