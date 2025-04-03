using System;
using System.Threading.Tasks;
using Aqt.CoreOracle.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.Categories;

public interface ICategoryTypeAppService : IApplicationService
{
    Task<CategoryTypeDto> GetAsync(Guid id);
    Task<CategoryTypeDto> GetByCodeAsync(string code);
    Task<PagedResultDto<CategoryTypeDto>> GetListAsync(CategoryTypeGetListInput input);
    Task<CategoryTypeDto> CreateAsync(CreateUpdateCategoryTypeDto input);
    Task<CategoryTypeDto> UpdateAsync(Guid id, CreateUpdateCategoryTypeDto input);
    Task DeleteAsync(Guid id);
} 