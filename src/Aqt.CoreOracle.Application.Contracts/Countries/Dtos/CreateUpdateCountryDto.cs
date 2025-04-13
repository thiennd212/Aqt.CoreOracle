using System.ComponentModel.DataAnnotations;
using Aqt.CoreOracle.Domain.Shared.Countries;

namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

public class CreateUpdateCountryDto
{
    [Required]
    [StringLength(CountryConsts.MaxCodeLength)]
    public string Code { get; set; }

    [Required]
    [StringLength(CountryConsts.MaxNameLength)]
    public string Name { get; set; }
} 