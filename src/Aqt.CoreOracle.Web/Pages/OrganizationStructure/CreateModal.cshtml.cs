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
    // Sử dụng permission tùy chỉnh đã định nghĩa
    [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Create)]
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid? ParentId { get; set; }

        [BindProperty]
        public OrganizationUnitCreateViewModel Input { get; set; }

        public List<SelectListItem> OrganizationUnitTypes { get; set; }

        private readonly ICustomOrganizationUnitAppService _organizationUnitAppService;
        private readonly IStringLocalizer<CoreOracleResource> _localizer;

        public CreateModalModel(
            ICustomOrganizationUnitAppService organizationUnitAppService,
            IStringLocalizer<CoreOracleResource> localizer)
        {
            _organizationUnitAppService = organizationUnitAppService;
            _localizer = localizer;
        }

        public virtual Task<IActionResult> OnGetAsync()
        {
            Input = new OrganizationUnitCreateViewModel
            {
                // Không cần gán ParentId ở đây vì nó đã được bind từ query string
                // ParentId = ParentId 
            };

            OrganizationUnitTypes = Enum.GetValues(typeof(OrganizationUnitTypeEnum))
                .Cast<OrganizationUnitTypeEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = _localizer[e.ToString()]
                    // Alternative localization key format if needed: Text = _localizer["Enum:OrganizationUnitTypeEnum:" + e.ToString()]
                }).ToList();

            return Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var dto = ObjectMapper.Map<OrganizationUnitCreateViewModel, CreateOrganizationUnitInput>(Input);
            // Gán ParentId từ thuộc tính của Model vào DTO
            dto.ParentId = ParentId;

            await _organizationUnitAppService.CreateAsync(dto);
            return NoContent();
        }

        public class OrganizationUnitCreateViewModel
        {
            // Bỏ ParentId khỏi ViewModel vì đã có thuộc tính ParentId ở PageModel
            // [HiddenInput]
            // public Guid? ParentId { get; set; } 

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