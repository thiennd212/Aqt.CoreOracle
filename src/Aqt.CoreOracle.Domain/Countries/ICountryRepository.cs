using System;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Domain.Countries;

public interface ICountryRepository : IRepository<Country, Guid>
{
    Task<Country?> FindByCodeAsync(
        string code,
        CancellationToken cancellationToken = default
    );

    Task<bool> CodeExistsAsync(
        string code,
        Guid? excludedId = null,
        CancellationToken cancellationToken = default
    );
} 