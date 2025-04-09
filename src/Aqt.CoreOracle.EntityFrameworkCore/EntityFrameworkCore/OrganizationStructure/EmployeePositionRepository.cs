using Aqt.CoreOracle.EntityFrameworkCore;
using Aqt.CoreOracle.Domain.OrganizationStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity; // Required for IdentityUser, OrganizationUnit in Where clauses/Includes
using Aqt.CoreOracle.Domain.Positions; // Corrected

namespace Aqt.CoreOracle.EntityFrameworkCore.OrganizationStructure
{
    public class EmployeePositionRepository
        : EfCoreRepository<CoreOracleDbContext, EmployeePosition, Guid>,
          IEmployeePositionRepository
    {
        public EmployeePositionRepository(
            IDbContextProvider<CoreOracleDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Override WithDetailsAsync to include related entities when requested
        public override async Task<IQueryable<EmployeePosition>> WithDetailsAsync()
        {
            // Assuming EmployeePosition has navigation properties: User, OrganizationUnit, Position
            // Adjust property names if they are different in your EmployeePosition entity
            return (await GetQueryableAsync()).Include(ep => ep.User)
                                               .Include(ep => ep.OrganizationUnit)
                                               .Include(ep => ep.Position);
        }

        public async Task<EmployeePosition?> FindAsync(
            Guid userId,
            Guid organizationUnitId,
            Guid positionId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = query.Where(ep => ep.UserId == userId &&
                                       ep.OrganizationUnitId == organizationUnitId &&
                                       ep.PositionId == positionId);

            if (includeDetails)
            {
                 // Assuming EmployeePosition has navigation properties: User, OrganizationUnit, Position
                 query = query.Include(ep => ep.User)
                              .Include(ep => ep.OrganizationUnit)
                              .Include(ep => ep.Position);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<EmployeePosition>> GetListByOrganizationUnitAsync(
            Guid organizationUnitId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var query = includeDetails
                ? await WithDetailsAsync() // Use overridden method for consistency
                : await GetQueryableAsync();

            return await query.Where(ep => ep.OrganizationUnitId == organizationUnitId)
                              .ToListAsync(cancellationToken);
        }

        public async Task<List<EmployeePosition>> GetListByUserIdAsync(
            Guid userId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
             var query = includeDetails
                ? await WithDetailsAsync()
                : await GetQueryableAsync();

            return await query.Where(ep => ep.UserId == userId)
                              .ToListAsync(cancellationToken);
        }

        public async Task<List<EmployeePosition>> GetListAsync(
            string? filter = null,
            Guid? organizationUnitId = null,
            Guid? userId = null,
            Guid? positionId = null,
            DateTime? activeAtDate = null,
            bool includeDetails = false,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            // Start with IQueryable for potential Includes
            var query = await GetQueryableAsync();

            if (includeDetails)
            {
                query = query.Include(ep => ep.User)
                             .Include(ep => ep.OrganizationUnit)
                             .Include(ep => ep.Position);
            }

            // Apply filters
            query = query
                .WhereIf(organizationUnitId.HasValue, ep => ep.OrganizationUnitId == organizationUnitId!.Value)
                .WhereIf(userId.HasValue, ep => ep.UserId == userId!.Value)
                .WhereIf(positionId.HasValue, ep => ep.PositionId == positionId!.Value)
                .WhereIf(activeAtDate.HasValue,
                         ep => ep.StartDate <= activeAtDate!.Value && (ep.EndDate == null || ep.EndDate >= activeAtDate!.Value));

            // Apply text filter
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Include(ep => ep.User).Include(ep => ep.Position); // Ensure User and Position are included for filtering

                query = query
                    .Where(ep => (ep.User != null && ep.User.Name.Contains(filter)) ||
                                (ep.Position != null && ep.Position.Name.Contains(filter)) ||
                                (ep.Position != null && ep.Position.Code.Contains(filter))
                          );

                 // If details are needed, OU should already be included from the start of the method.
                 // No complex check needed here.
            }

            // Apply sorting
            query = query.OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(EmployeePosition.CreationTime) + " DESC" : sorting);

            // Apply paging
            return await query.PageBy(skipCount, maxResultCount)
                              .ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string? filter = null,
            Guid? organizationUnitId = null,
            Guid? userId = null,
            Guid? positionId = null,
            DateTime? activeAtDate = null,
            CancellationToken cancellationToken = default)
        {
            // Start with IQueryable for potential Includes needed for filtering
             var query = await GetQueryableAsync();

            // Apply filters (same logic as GetListAsync)
            query = query
                .WhereIf(organizationUnitId.HasValue, ep => ep.OrganizationUnitId == organizationUnitId!.Value)
                .WhereIf(userId.HasValue, ep => ep.UserId == userId!.Value)
                .WhereIf(positionId.HasValue, ep => ep.PositionId == positionId!.Value)
                .WhereIf(activeAtDate.HasValue,
                         ep => ep.StartDate <= activeAtDate!.Value && (ep.EndDate == null || ep.EndDate >= activeAtDate!.Value));

             if (!string.IsNullOrWhiteSpace(filter))
            {
                // Ensure related entities are available for filtering
                query = query.Include(ep => ep.User).Include(ep => ep.Position);
                query = query
                    .Where(ep => (ep.User != null && ep.User.Name.Contains(filter)) ||
                                (ep.Position != null && ep.Position.Name.Contains(filter)) ||
                                (ep.Position != null && ep.Position.Code.Contains(filter))
                          );
                // Other filters are already applied above
            }

            return await query.LongCountAsync(cancellationToken);
        }
    }
} 