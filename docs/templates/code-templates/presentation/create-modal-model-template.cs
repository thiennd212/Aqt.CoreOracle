using System.Threading.Tasks;
using Aqt.CoreOracle.[ModuleName];
using Aqt.CoreOracle.[ModuleName].Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Aqt.CoreOracle.Web.Pages.[ModuleName].[EntityName]s
{
    /// <summary>
    /// Page model for creating a new [EntityName]
    /// </summary>
    public class CreateModalModel : CoreOraclePageModel
    {
        [BindProperty]
        public [EntityName]CreateUpdateDto [EntityName] { get; set; }

        private readonly I[EntityName]AppService _[entityName]AppService;

        public CreateModalModel(I[EntityName]AppService [entityName]AppService)
        {
            _[entityName]AppService = [entityName]AppService;
        }

        public void OnGet()
        {
            [EntityName] = new [EntityName]CreateUpdateDto
            {
                IsActive = true
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _[entityName]AppService.CreateAsync([EntityName]);
            return NoContent();
        }
    }
} 