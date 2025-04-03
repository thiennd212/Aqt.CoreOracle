using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// Repository interface for [EntityName]
    /// </summary>
    public interface I[EntityName]Repository : IRepository<[EntityName], Guid>
    {
        /// <summary>
        /// Gets an entity by its code
        /// </summary>
        /// <param name="code">The code to search for</param>
        /// <param name="includeDetails">Whether to include details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<[EntityName]> GetByCodeAsync(
            string code,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets a list of entities based on the given filter
        /// </summary>
        /// <param name="filter">Text to filter name or code</param>
        /// <param name="isActive">Filter by active status</param>
        /// <param name="sorting">Sort expression</param>
        /// <param name="maxResultCount">Max number of results</param>
        /// <param name="skipCount">Number of results to skip</param>
        /// <param name="includeDetails">Whether to include details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities matching the criteria</returns>
        Task<List<[EntityName]>> GetListAsync(
            string filter = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets the total count of entities matching the filter
        /// </summary>
        /// <param name="filter">Text to filter name or code</param>
        /// <param name="isActive">Filter by active status</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Total count of matching entities</returns>
        Task<long> GetCountAsync(
            string filter = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Checks if an entity with the given code exists
        /// </summary>
        /// <param name="code">The code to check</param>
        /// <param name="expectedId">Expected entity id (optional, for update)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if exists, false otherwise</returns>
        Task<bool> ExistsByCodeAsync(
            string code,
            Guid? expectedId = null,
            CancellationToken cancellationToken = default
        );
    }
} 