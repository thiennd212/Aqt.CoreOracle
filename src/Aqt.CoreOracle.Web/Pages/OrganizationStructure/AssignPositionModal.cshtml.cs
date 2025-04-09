using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Positions;
using Aqt.CoreOracle.OrganizationUnits;
using Aqt.CoreOracle.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Microsoft.Extensions.Localization;
using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

namespace Aqt.CoreOracle.Web.Pages.OrganizationStructure
{
    [Authorize(CoreOraclePermissions.OrganizationManagement.AssignPositions)]
    public class AssignPositionModalModel : AbpPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid OrganizationUnitId { get; set; }

        [BindProperty]
        public AssignPositionViewModel Input { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Positions { get; set; }

        private readonly ICustomOrganizationUnitAppService _organizationUnitAppService;
        private readonly IPositionAppService _positionAppService;
        private readonly IIdentityUserAppService _userLookupService;
        private readonly IStringLocalizer<CoreOracleResource> _localizer;

        public AssignPositionModalModel(
            ICustomOrganizationUnitAppService organizationUnitAppService,
            IPositionAppService positionAppService,
            IIdentityUserAppService userLookupService,
            IStringLocalizer<CoreOracleResource> localizer)
        {
            _organizationUnitAppService = organizationUnitAppService;
            _positionAppService = positionAppService;
            _userLookupService = userLookupService;
            _localizer = localizer;
        }

        public virtual async Task OnGetAsync()
        {
            Input = new AssignPositionViewModel
            {
                StartDate = Clock.Now // Default StartDate to today
            };

            // Get Positions
            var positionList = await _positionAppService.GetListAsync(new GetPositionListInput { MaxResultCount = 1000 }); // Adjust MaxResultCount if needed
            Positions = positionList.Items.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.Name} ({p.Code})"
            }).ToList();

            // Get Users - Consider filtering out already assigned users in a real application
            var userLookup = await _userLookupService.GetListAsync(new GetIdentityUsersInput { MaxResultCount = 1000 }); // Adjust MaxResultCount
            Users = userLookup.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.UserName ?? u.Name // Display UserName or Name
            }).OrderBy(u => u.Text).ToList();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            // Custom Validation: EndDate must be greater than StartDate if provided
            if (Input.EndDate.HasValue && Input.EndDate.Value <= Input.StartDate)
            {
                ModelState.AddModelError("Input.EndDate", _localizer["CoreOracle:00005"]); // Use defined error code
                // Reload lists for the form
                await OnGetAsync(); // Re-populate Users and Positions lists before returning Page
                return Page();
            }

            var dto = ObjectMapper.Map<AssignPositionViewModel, AssignEmployeePositionDto>(Input);
            dto.OrganizationUnitId = OrganizationUnitId;

            try
            {
                await _organizationUnitAppService.AssignPositionToUserAsync(dto);
                return NoContent();
            }
            catch (Volo.Abp.BusinessException ex)
            {
                // Handle specific business exceptions (e.g., already assigned)
                 ModelState.AddModelError(string.Empty, ex.Message);
                 await OnGetAsync(); // Re-populate lists
                 return Page();
            }
        }

        // ViewModel for Assigning Position
        public class AssignPositionViewModel
        {
            [Required]
            [Display(Name = "User")]
            public Guid UserId { get; set; }

            [Required]
            [Display(Name = "Position")]
            public Guid PositionId { get; set; }

            [Required]
            [Display(Name = "StartDate")]
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; }

            [Display(Name = "EndDate")]
            [DataType(DataType.Date)]
            public DateTime? EndDate { get; set; }
        }
    }
} 