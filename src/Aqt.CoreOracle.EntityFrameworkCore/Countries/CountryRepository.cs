using System;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.Domain.Countries;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Aqt.CoreOracle.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aqt.CoreOracle.EntityFrameworkCore.Countries;

public class CountryRepository :
    EfCoreRepository<CoreOracleDbContext, Country, Guid>,
    ICountryRepository
{
    public CountryRepository(IDbContextProvider<CoreOracleDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Country?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .FirstOrDefaultAsync(x => x.Code == code, GetCancellationToken(cancellationToken));
    }

    public async Task<bool> CodeExistsAsync(string code, Guid? excludedId = null, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .AnyAsync(x => x.Code == code && (!excludedId.HasValue || x.Id != excludedId.Value),
                GetCancellationToken(cancellationToken));
    }
} 