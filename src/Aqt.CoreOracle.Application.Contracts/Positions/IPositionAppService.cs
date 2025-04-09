using System;
using Volo.Abp.Application.Services;
using Aqt.CoreOracle.Application.Contracts.Positions;

namespace Aqt.CoreOracle.Application.Contracts.Positions;

public interface IPositionAppService : 
    ICrudAppService<
        PositionDto,                  // DTO để hiển thị
        Guid,                         // Primary key
        GetPositionListInput,         // DTO dùng cho filtering/sorting
        CreateUpdatePositionDto,      // DTO dùng cho creating/updating
        CreateUpdatePositionDto>      // DTO dùng cho updating (có thể khác với creating)
{
} 