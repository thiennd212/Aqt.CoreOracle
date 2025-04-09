using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits
{
    public class GetOrganizationUnitInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
} 