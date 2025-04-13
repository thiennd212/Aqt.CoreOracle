using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aqt.CoreOracle.Domain.Shared.Provinces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Aqt.CoreOracle.Web.Pages.Provinces.ViewModels;

public class CreateProvinceViewModel
{
    [Required]
    [Display(Name = "ProvinceCountry")]
    [SelectItems(nameof(CreateModalModel.CountryLookup))] // Points to the PageModel property
    public Guid CountryId { get; set; }

    [Required]
    [Display(Name = "ProvinceCode")]
    [StringLength(ProvinceConsts.MaxCodeLength)]
    public string Code { get; set; }

    [Required]
    [Display(Name = "ProvinceName")]
    [StringLength(ProvinceConsts.MaxNameLength)]
    public string Name { get; set; }
} 