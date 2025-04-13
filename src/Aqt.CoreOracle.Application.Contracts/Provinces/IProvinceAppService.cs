using System;
using System.Threading.Tasks;
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.Application.Contracts.Provinces;

public interface IProvinceAppService :
    ICrudAppService<
        ProvinceDto,
        Guid,
        GetProvincesInput,
        CreateUpdateProvinceDto>
{
    Task<ListResultDto<ProvinceLookupDto>> GetLookupAsync(Guid countryId);
} 