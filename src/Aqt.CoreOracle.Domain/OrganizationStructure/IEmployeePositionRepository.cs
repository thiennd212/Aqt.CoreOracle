using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Domain.OrganizationStructure
{
    public interface IEmployeePositionRepository : IRepository<EmployeePosition, Guid>
    {
        Task<EmployeePosition?> FindAsync(
            Guid userId,
            Guid organizationUnitId,
            Guid positionId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeePosition>> GetListByOrganizationUnitAsync(
            Guid organizationUnitId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeePosition>> GetListByUserIdAsync(
            Guid userId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeePosition>> GetListAsync(
            string? filter = null,
            Guid? organizationUnitId = null,
            Guid? userId = null,
            Guid? positionId = null,
            DateTime? activeAtDate = null,
            bool includeDetails = false,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filter = null,
            Guid? organizationUnitId = null,
            Guid? userId = null,
            Guid? positionId = null,
            DateTime? activeAtDate = null,
            CancellationToken cancellationToken = default
        );
    }
} 