using System;
using System.Threading.Tasks;
using Aqt.CoreOracle.[ModuleName];
using Aqt.CoreOracle.[ModuleName].Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Aqt.CoreOracle.Web.Pages.[ModuleName].[EntityName]s
{
    /// <summary>
    /// Page model for editing an existing [EntityName]
    /// </summary>
    public class EditModalModel : CoreOraclePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public [EntityName]CreateUpdateDto [EntityName] { get; set; }

        private readonly I[EntityName]AppService _[entityName]AppService;

        public EditModalModel(I[EntityName]AppService [entityName]AppService)
        {
            _[entityName]AppService = [entityName]AppService;
        }

        public async Task OnGetAsync()
        {
            var [entityName] = await _[entityName]AppService.GetAsync(Id);
            [EntityName] = ObjectMapper.Map<[EntityName]ListDto, [EntityName]CreateUpdateDto>([entityName]);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _[entityName]AppService.UpdateAsync(Id, [EntityName]);
            return NoContent();
        }
    }
}