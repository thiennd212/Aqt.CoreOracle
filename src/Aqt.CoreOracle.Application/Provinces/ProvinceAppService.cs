using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Provinces;
using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Aqt.CoreOracle.Domain.Countries;
using Aqt.CoreOracle.Domain.Provinces;
using Aqt.CoreOracle.Domain.Provinces.Entities;
using Aqt.CoreOracle.Domain.Shared;
using Aqt.CoreOracle.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Application.Provinces;

[Authorize(CoreOraclePermissions.Provinces.Default)]
public class ProvinceAppService :
    CrudAppService<Province, ProvinceDto, Guid, GetProvincesInput, CreateUpdateProvinceDto>,
    IProvinceAppService
{
    private readonly IProvinceRepository _provinceRepository;
    private readonly ICountryRepository _countryRepository;

    public ProvinceAppService(
        IProvinceRepository provinceRepository,
        ICountryRepository countryRepository)
        : base(provinceRepository)
    {
        _provinceRepository = provinceRepository;
        _countryRepository = countryRepository;

        GetPolicyName = CoreOraclePermissions.Provinces.Default;
        GetListPolicyName = CoreOraclePermissions.Provinces.Default;
        CreatePolicyName = CoreOraclePermissions.Provinces.Create;
        UpdatePolicyName = CoreOraclePermissions.Provinces.Edit;
        DeletePolicyName = CoreOraclePermissions.Provinces.Delete;
    }

    protected override async Task<IQueryable<Province>> CreateFilteredQueryAsync(GetProvincesInput input)
    {
        // Use IProvinceRepository for optimized query if available, otherwise fall back to base
        // Example assumes IProvinceRepository has GetQueryableAsync()
        var query = (await _provinceRepository.GetQueryableAsync());

        if (input.CountryId.HasValue)
        {
            query = query.Where(p => p.CountryId == input.CountryId.Value);
        }

        if (!input.Filter.IsNullOrWhiteSpace())
        {
            query = query.Where(p =>
                p.Code.Contains(input.Filter) || p.Name.Contains(input.Filter));
        }

        return query;
    }

    public override async Task<PagedResultDto<ProvinceDto>> GetListAsync(GetProvincesInput input)
    {
        await CheckGetListPolicyAsync();

        // Use specialized repository method if available
        var totalCount = await _provinceRepository.GetCountAsync(input.Filter, input.CountryId);
        var provinces = await _provinceRepository.GetListWithCountryAsync(
            input.Filter,
            input.CountryId,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount
        );

        var dtos = ObjectMapper.Map<List<Province>, List<ProvinceDto>>(provinces);
        return new PagedResultDto<ProvinceDto>(totalCount, dtos);
    }

    [Authorize(CoreOraclePermissions.Provinces.Create)]
    public override async Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input)
    {
        await CheckCreatePolicyAsync();
        await ValidateCountryExistsAsync(input.CountryId);
        await CheckCodeExistsInCountryAsync(input.Code, input.CountryId);

        var province = new Province(GuidGenerator.Create(), input.CountryId, input.Code, input.Name);
        // Note: ObjectMapper might not be suitable here if we need the constructor logic of Province
        // Consider manual mapping or a specific mapping profile if constructor logic is complex

        await _provinceRepository.InsertAsync(province, autoSave: true);
        await _provinceRepository.EnsurePropertyLoadedAsync(province, p => p.Country); // Load Country for the return DTO

        return ObjectMapper.Map<Province, ProvinceDto>(province);
    }

    [Authorize(CoreOraclePermissions.Provinces.Edit)]
    public override async Task<ProvinceDto> UpdateAsync(Guid id, CreateUpdateProvinceDto input)
    {
        await CheckUpdatePolicyAsync();

        var province = await _provinceRepository.GetAsync(id);
        await ValidateCountryExistsAsync(input.CountryId);
        await CheckCodeExistsInCountryAsync(input.Code, input.CountryId, province.Id);

        // Use internal methods for controlled updates
        province.SetCountry(input.CountryId);
        province.SetCode(input.Code);
        province.SetName(input.Name);
        // ObjectMapper.Map(input, province); // Avoid direct mapping if internal setters are preferred

        await _provinceRepository.UpdateAsync(province, autoSave: true);
        await _provinceRepository.EnsurePropertyLoadedAsync(province, p => p.Country); // Load Country for the return DTO

        return ObjectMapper.Map<Province, ProvinceDto>(province);
    }

    [Authorize(CoreOraclePermissions.Provinces.Delete)]
    public override Task DeleteAsync(Guid id)
    {
        // Could add extra logic here if needed before deleting
        return base.DeleteAsync(id);
    }

    private async Task ValidateCountryExistsAsync(Guid countryId)
    {
        if (!await _countryRepository.AnyAsync(c => c.Id == countryId))
        {
            throw new UserFriendlyException(L[CoreOracleDomainErrorCodes.CountryNotFoundForProvince]);
        }
    }

    private async Task CheckCodeExistsInCountryAsync(string code, Guid countryId, Guid? excludedId = null)
    {
        if (await _provinceRepository.CodeExistsAsync(code, countryId, excludedId))
        {
            throw new UserFriendlyException(
                L[CoreOracleDomainErrorCodes.ProvinceCodeAlreadyExistsInCountry, code]
            );
        }
    }

    // GetLookupAsync for dropdowns etc.
    public async Task<ListResultDto<ProvinceLookupDto>> GetLookupAsync(Guid countryId)
    {
        var provinces = await _provinceRepository.GetListByCountryAsync(countryId);
        // Sorting is handled by GetListByCountryAsync if specified, otherwise default sort here
        // provinces = provinces.OrderBy(p => p.Name).ToList(); 
        return new ListResultDto<ProvinceLookupDto>(
            ObjectMapper.Map<List<Province>, List<ProvinceLookupDto>>(provinces)
        );
    }
} 