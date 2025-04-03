using System;
using System.Threading.Tasks;
using Aqt.CoreOracle.[ModuleName].Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// Application service interface for managing [EntityName]s
    /// </summary>
    public interface I[EntityName]AppService :
        ICrudAppService<
            [EntityName]ListDto,
            Guid,
            PagedAndSortedResultRequestDto,
            [EntityName]CreateUpdateDto,
            [EntityName]CreateUpdateDto>
    {
        /// <summary>
        /// Gets a [EntityName] by its code
        /// </summary>
        /// <param name="code">The code to search for</param>
        /// <returns>The [EntityName] if found</returns>
        Task<[EntityName]ListDto> GetByCodeAsync(string code);

        /// <summary>
        /// Gets a list of [EntityName]s for lookup
        /// </summary>
        /// <param name="filter">Optional text to filter by name or code</param>
        /// <param name="isActive">Optional filter by active status</param>
        /// <returns>List of [EntityName] lookup items</returns>
        Task<ListResultDto<[EntityName]LookupDto>> GetLookupAsync(
            string filter = null,
            bool? isActive = null);
    }
} 