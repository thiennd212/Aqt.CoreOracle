using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.Application.Contracts.Positions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Domain.Entities;

namespace Aqt.CoreOracle.Web.Pages.Positions;

[Authorize(CoreOraclePermissions.OrganizationManagement.Positions.Create)]
public class CreateModalModel : AbpPageModel
{
    [BindProperty]
    public PositionCreateViewModel Input { get; set; }

    private readonly IPositionAppService _positionAppService;

    public CreateModalModel(IPositionAppService positionAppService)
    {
        _positionAppService = positionAppService;
    }

    public virtual void OnGet()
    {
        Input = new PositionCreateViewModel();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<PositionCreateViewModel, CreateUpdatePositionDto>(Input);
        await _positionAppService.CreateAsync(dto);
        return NoContent();
    }

    public class PositionCreateViewModel
    {
        [Required]
        [StringLength(CoreOracleConsts.MaxPositionNameLength)]
        [Display(Name = "Position:Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(CoreOracleConsts.MaxPositionCodeLength)]
        [Display(Name = "Position:Code")]
        public string Code { get; set; }

        [StringLength(CoreOracleConsts.MaxDescriptionLength)] // Assuming a general max length, adjust if needed
        [DataType(DataType.MultilineText)]
        [Display(Name = "Position:Description")]
        public string? Description { get; set; }
    }
} 