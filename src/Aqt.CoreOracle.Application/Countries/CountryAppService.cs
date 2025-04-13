using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.Application.Contracts.Countries;
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
using Aqt.CoreOracle.Domain.Countries;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Aqt.CoreOracle.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.Application.Countries;

[Authorize(CoreOraclePermissions.Countries.Default)]
public class CountryAppService : 
    CrudAppService<Country, CountryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCountryDto>,
    ICountryAppService
{
    private readonly ICountryRepository _countryRepository;

    public CountryAppService(ICountryRepository countryRepository) 
        : base(countryRepository)
    {
        _countryRepository = countryRepository;
        
        GetPolicyName = CoreOraclePermissions.Countries.Default;
        GetListPolicyName = CoreOraclePermissions.Countries.Default;
        CreatePolicyName = CoreOraclePermissions.Countries.Create;
        UpdatePolicyName = CoreOraclePermissions.Countries.Edit;
        DeletePolicyName = CoreOraclePermissions.Countries.Delete;
    }

    [Authorize(CoreOraclePermissions.Countries.Create)]
    public override async Task<CountryDto> CreateAsync(CreateUpdateCountryDto input)
    {
        await CheckCodeExistsAsync(input.Code);
        
        var country = await MapToEntityAsync(input);
        
        await Repository.InsertAsync(country, autoSave: true);

        return await MapToGetOutputDtoAsync(country);
    }

    [Authorize(CoreOraclePermissions.Countries.Edit)]
    public override async Task<CountryDto> UpdateAsync(Guid id, CreateUpdateCountryDto input)
    {
        await CheckCodeExistsAsync(input.Code, id);
        
        var country = await GetEntityByIdAsync(id);
        
        await MapToEntityAsync(input, country);
        
        await Repository.UpdateAsync(country, autoSave: true);

        return await MapToGetOutputDtoAsync(country);
    }

    private async Task CheckCodeExistsAsync(string code, Guid? excludedId = null)
    {
        if (await _countryRepository.CodeExistsAsync(code, excludedId))
        {
            throw new UserFriendlyException(L[CoreOracleDomainErrorCodes.CountryCodeAlreadyExists]);
        }
    }

    public async Task<ListResultDto<CountryLookupDto>> GetLookupAsync()
    {
        await CheckGetListPolicyAsync();

        var countries = await _countryRepository.GetListAsync();
        countries = countries.OrderBy(c => c.Name).ToList();

        return new ListResultDto<CountryLookupDto>(
            ObjectMapper.Map<List<Country>, List<CountryLookupDto>>(countries)
        );
    }
} 