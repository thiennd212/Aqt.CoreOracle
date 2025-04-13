using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.Domain.Provinces;
using Aqt.CoreOracle.Domain.Provinces.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aqt.CoreOracle.EntityFrameworkCore.Provinces;

public class ProvinceRepository :
    EfCoreRepository<CoreOracleDbContext, Province, Guid>,
    IProvinceRepository
{
    public ProvinceRepository(IDbContextProvider<CoreOracleDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Province?> FindByCodeAsync(
        string code,
        Guid countryId,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.CountryId == countryId && x.Code == code,
                GetCancellationToken(cancellationToken));
    }

    public async Task<bool> CodeExistsAsync(
        string code,
        Guid countryId,
        Guid? excludedId = null,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .AsNoTracking()
            .AnyAsync(x =>
                x.CountryId == countryId &&
                x.Code == code &&
                (!excludedId.HasValue || x.Id != excludedId.Value),
                GetCancellationToken(cancellationToken));
    }

    public async Task<List<Province>> GetListByCountryAsync(
        Guid countryId,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet
            .AsNoTracking()
            .Where(x => x.CountryId == countryId);

        query = query.OrderBy(sorting.IsNullOrWhiteSpace()
            ? nameof(Province.Name) + " asc"
            : sorting);

        return await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetCountByCountryAsync(
        Guid countryId,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .AsNoTracking()
            .LongCountAsync(x => x.CountryId == countryId,
                GetCancellationToken(cancellationToken));
    }

    public async Task<List<Province>> GetListWithCountryAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet
            .AsNoTracking()
            .Include(p => p.Country)
            .WhereIf(countryId.HasValue, p => p.CountryId == countryId.Value)
            .WhereIf(!filterText.IsNullOrWhiteSpace(),
                p => p.Code.Contains(filterText!) || p.Name.Contains(filterText!));

        query = query.OrderBy(sorting.IsNullOrWhiteSpace()
            ? $"{nameof(Province.Country)}.{nameof(Domain.Countries.Entities.Country.Name)} asc, {nameof(Province.Name)} asc"
            : sorting);

        return await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetCountAsync(
        string? filterText = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet
            .AsNoTracking()
            .WhereIf(countryId.HasValue, p => p.CountryId == countryId.Value)
            .WhereIf(!filterText.IsNullOrWhiteSpace(),
                p => p.Code.Contains(filterText!) || p.Name.Contains(filterText!));

        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }
} 