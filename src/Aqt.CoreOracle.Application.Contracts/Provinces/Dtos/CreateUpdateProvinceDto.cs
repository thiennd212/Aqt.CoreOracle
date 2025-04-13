using System;
using System.ComponentModel.DataAnnotations;
using Aqt.CoreOracle.Domain.Shared.Provinces;

namespace Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;

public class CreateUpdateProvinceDto
{
    [Required]
    public Guid CountryId { get; set; }

    [Required]
    [StringLength(ProvinceConsts.MaxCodeLength)]
    public string Code { get; set; }

    [Required]
    [StringLength(ProvinceConsts.MaxNameLength)]
    public string Name { get; set; }
} 