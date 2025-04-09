using Aqt.CoreOracle.EntityFrameworkCore;
using Aqt.CoreOracle.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aqt.CoreOracle.EntityFrameworkCore.Positions
{
    public class PositionRepository
        : EfCoreRepository<CoreOracleDbContext, Position, Guid>,
          IPositionRepository
    {
        public PositionRepository(
            IDbContextProvider<CoreOracleDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Position?> FindByCodeAsync(
            string code,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
        }

        public async Task<bool> CodeExistsAsync(
            string code,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(excludeId.HasValue, p => p.Id != excludeId!.Value)
                .AnyAsync(p => p.Code == code, cancellationToken);
        }

        public async Task<List<Position>> GetListAsync(
            string? filter = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(filter),
                         p => p.Name.Contains(filter!) || p.Code.Contains(filter!));

            query = query.OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Position.Name) : sorting);

            return await query.PageBy(skipCount, maxResultCount)
                              .ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default)
        {
             var query = await GetQueryableAsync();

            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(filter),
                         p => p.Name.Contains(filter!) || p.Code.Contains(filter!));

            return await query.LongCountAsync(cancellationToken);
        }
    }
} 