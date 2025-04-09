using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.Application.Contracts.Positions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Domain.Entities;

namespace Aqt.CoreOracle.Web.Pages.Positions;

[Authorize(CoreOraclePermissions.OrganizationManagement.Positions.Update)]
public class EditModalModel : AbpPageModel
{
    [BindProperty]
    public PositionEditViewModel Input { get; set; }

    private readonly IPositionAppService _positionAppService;

    public EditModalModel(IPositionAppService positionAppService)
    {
        _positionAppService = positionAppService;
    }

    public virtual async Task OnGetAsync(Guid id)
    {
        var positionDto = await _positionAppService.GetAsync(id);
        Input = ObjectMapper.Map<PositionDto, PositionEditViewModel>(positionDto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<PositionEditViewModel, CreateUpdatePositionDto>(Input);
        await _positionAppService.UpdateAsync(Input.Id, dto);
        return NoContent();
    }

    public class PositionEditViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(CoreOracleConsts.MaxPositionNameLength)]
        [Display(Name = "Position:Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(CoreOracleConsts.MaxPositionCodeLength)]
        [Display(Name = "Position:Code")]
        public string Code { get; set; }

        [StringLength(CoreOracleConsts.MaxDescriptionLength)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Position:Description")]
        public string? Description { get; set; }
    }
} 