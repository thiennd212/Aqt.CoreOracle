using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aqt.CoreOracle.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.Categories;

public interface ICategoryItemAppService : IApplicationService
{
    Task<CategoryItemDto> GetAsync(Guid id);
    Task<CategoryItemDto> GetByCodeAsync(Guid categoryTypeId, string code);
    Task<PagedResultDto<CategoryItemDto>> GetListAsync(CategoryItemGetListInput input);
    Task<List<CategoryItemDto>> GetListByTypeCodeAsync(string categoryTypeCode);
    Task<CategoryItemDto> CreateAsync(CreateUpdateCategoryItemDto input);
    Task<CategoryItemDto> UpdateAsync(Guid id, CreateUpdateCategoryItemDto input);
    Task DeleteAsync(Guid id);
    Task<bool> IsCodeExistAsync(Guid categoryTypeId, string code, Guid? expectedId = null);
} 