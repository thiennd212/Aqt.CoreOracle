using System;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.Application.Contracts.Countries;

public interface ICountryAppService :
    ICrudAppService<
        CountryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCountryDto>
{
    Task<ListResultDto<CountryLookupDto>> GetLookupAsync();
} 