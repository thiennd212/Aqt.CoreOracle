using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aqt.CoreOracle.Application.Contracts.Countries; // Namespace for ICountryAppService
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos; // Namespace for DTOs
using Aqt.CoreOracle.Web.Pages; // Namespace for CoreOraclePageModel

namespace Aqt.CoreOracle.Web.Pages.Countries;

public class EditModalModel : CoreOraclePageModel
{
    [HiddenInput] // Keep Id hidden in the form but available for binding
    [BindProperty(SupportsGet = true)] // Bind from query string on GET
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateCountryDto? Country { get; set; } // Made nullable

    private readonly ICountryAppService _countryAppService;

    public EditModalModel(ICountryAppService countryAppService)
    {
        _countryAppService = countryAppService;
        // ObjectMapper is inherited from CoreOraclePageModel / AbpPageModel
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _countryAppService.GetAsync(Id); // Get the country details
        // Map from the read DTO (CountryDto) to the CreateUpdate DTO for the form
        Country = ObjectMapper.Map<CountryDto, CreateUpdateCountryDto>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        // Add null check or use null-forgiving operator if Country can realistically be null after binding
        await _countryAppService.UpdateAsync(Id, Country!); // Used null-forgiving as it's populated in OnGetAsync
        return NoContent(); // Indicate success with no content
    }
} 