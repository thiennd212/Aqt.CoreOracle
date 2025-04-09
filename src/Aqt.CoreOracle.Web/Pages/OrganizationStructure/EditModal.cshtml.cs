using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.OrganizationUnits;
using Aqt.CoreOracle.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.ObjectExtending;
using Aqt.CoreOracle.Domain.Shared;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;
using Microsoft.Extensions.Localization;
using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

namespace Aqt.CoreOracle.Web.Pages.OrganizationStructure
{
    [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Update)]
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public OrganizationUnitEditViewModel Input { get; set; }

        public List<SelectListItem> OrganizationUnitTypes { get; set; }

        private readonly ICustomOrganizationUnitAppService _organizationUnitAppService;
        private readonly IStringLocalizer<CoreOracleResource> _localizer;

        public EditModalModel(
            ICustomOrganizationUnitAppService organizationUnitAppService,
            IStringLocalizer<CoreOracleResource> localizer)
        {
            _organizationUnitAppService = organizationUnitAppService;
            _localizer = localizer;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            var ouDto = await _organizationUnitAppService.GetAsync(Id);
            Input = ObjectMapper.Map<CustomOrganizationUnitDto, OrganizationUnitEditViewModel>(ouDto);

            OrganizationUnitTypes = Enum.GetValues(typeof(OrganizationUnitTypeEnum))
                .Cast<OrganizationUnitTypeEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = _localizer[e.ToString()],
                    Selected = e == Input.OrganizationUnitType // Set selected item
                }).ToList();

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var dto = ObjectMapper.Map<OrganizationUnitEditViewModel, UpdateOrganizationUnitInput>(Input);
            await _organizationUnitAppService.UpdateAsync(Id, dto);

            return NoContent();
        }

        // ViewModel for editing an Organization Unit
        public class OrganizationUnitEditViewModel
        {
            [Required]
            [Display(Name = "OrganizationUnitDisplayName")]
            [StringLength(CoreOracleConsts.MaxOrganizationUnitDisplayNameLength)] // Sử dụng hằng số
            public string DisplayName { get; set; }

            [Display(Name = "OrganizationUnitAddress")]
            [StringLength(CoreOracleConsts.MaxOrganizationUnitAddressLength)]
            public string Address { get; set; }

            [Display(Name = "OrganizationUnitSyncCode")]
            [StringLength(CoreOracleConsts.MaxOrganizationUnitSyncCodeLength)]
            public string SyncCode { get; set; }

            [Required]
            [Display(Name = "OrganizationUnitType")]
            public OrganizationUnitTypeEnum OrganizationUnitType { get; set; }
        }
    }
} 