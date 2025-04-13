using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.Domain.Provinces.Entities;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Domain.Provinces;

public interface IProvinceRepository : IRepository<Province, Guid>
{
    Task<Province?> FindByCodeAsync(
        string code,
        Guid countryId,
        CancellationToken cancellationToken = default);

    Task<bool> CodeExistsAsync(
        string code,
        Guid countryId,
        Guid? excludedId = null,
        CancellationToken cancellationToken = default);

    Task<List<Province>> GetListByCountryAsync(
        Guid countryId,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountByCountryAsync(
        Guid countryId,
        CancellationToken cancellationToken = default);

    Task<List<Province>> GetListWithCountryAsync(
        string? filterText = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        string? filterText = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);
} 