using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.[ModuleName];
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aqt.CoreOracle.EntityFrameworkCore.[ModuleName]
{
    /// <summary>
    /// Repository implementation for [EntityName]
    /// </summary>
    public class EfCore[EntityName]Repository : EfCoreRepository<CoreOracleDbContext, [EntityName], Guid>, I[EntityName]Repository
    {
        public EfCore[EntityName]Repository(IDbContextProvider<CoreOracleDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<[EntityName]> GetByCodeAsync(
            string code,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var query = includeDetails
                ? await WithDetailsAsync()
                : await GetQueryableAsync();

            return await query
                .Where(x => x.Code == code)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<[EntityName]>> GetListAsync(
            string filter = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var query = includeDetails
                ? await WithDetailsAsync()
                : await GetQueryableAsync();

            return await ApplyFilter(query, filter, isActive)
                .OrderBy(sorting ?? nameof([EntityName].DisplayOrder))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string filter = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            return await ApplyFilter(query, filter, isActive)
                .LongCountAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistsByCodeAsync(
            string code,
            Guid? expectedId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            return await query
                .Where(x => x.Code == code)
                .WhereIf(expectedId.HasValue, x => x.Id != expectedId.Value)
                .AnyAsync(cancellationToken);
        }

        protected virtual IQueryable<[EntityName]> ApplyFilter(
            IQueryable<[EntityName]> query,
            string filter,
            bool? isActive)
        {
            return query
                .WhereIf(!filter.IsNullOrWhiteSpace(), x =>
                    x.Code.Contains(filter) ||
                    x.Name.Contains(filter))
                .WhereIf(isActive.HasValue, x => x.IsActive == isActive);
        }
    }
} 