using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Domain.Positions;

public interface IPositionRepository : IRepository<Position, Guid>
{
    Task<Position?> FindByCodeAsync(
        string code,
        CancellationToken cancellationToken = default
    );

    Task<bool> CodeExistsAsync(
        string code,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default
    );

    Task<List<Position>> GetListAsync(
        string? filter = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string? filter = null,
        CancellationToken cancellationToken = default
    );
} 