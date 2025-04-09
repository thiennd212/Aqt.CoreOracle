using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

namespace Aqt.CoreOracle.OrganizationUnits
{
    public interface ICustomOrganizationUnitAppService : IApplicationService
    {
        Task<CustomOrganizationUnitDto> GetAsync(Guid id);
        Task<PagedResultDto<CustomOrganizationUnitDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<ListResultDto<CustomOrganizationUnitDto>> GetListAllAsync();
        Task<CustomOrganizationUnitDto> CreateAsync(CreateOrganizationUnitInput input);
        Task<CustomOrganizationUnitDto> UpdateAsync(Guid id, UpdateOrganizationUnitInput input);
        Task DeleteAsync(Guid id);
        Task<CustomOrganizationUnitDto> MoveAsync(Guid id, Guid? parentId);
        Task<ListResultDto<CustomOrganizationUnitDto>> GetChildrenAsync(Guid parentId);
        Task<ListResultDto<CustomOrganizationUnitDto>> GetRootListAsync();
        Task<CustomOrganizationUnitDto> GetRootAsync();
        Task<ListResultDto<CustomOrganizationUnitDto>> GetRootsAsync();
        Task<ListResultDto<CustomOrganizationUnitDto>> GetRootNodesAsync();
        Task<CustomOrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId);
        Task<CustomOrganizationUnitDto> GetOuByCodeAsync(string code);
        Task<string> GetNextChildCodeAsync(Guid? parentId);
        Task<string> GetCodeAsync(Guid id);
        Task<ListResultDto<OrganizationUnitTreeNodeDto>> GetOrganizationTreeAsync();
        Task<PagedResultDto<EmployeePositionDto>> GetEmployeePositionsAsync(GetOuEmployeePositionsInput input);
        Task AssignPositionToUserAsync(AssignEmployeePositionDto input);
        Task RemovePositionFromUserAsync(Guid employeePositionId);
    }
}