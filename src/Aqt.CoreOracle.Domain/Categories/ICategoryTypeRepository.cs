using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Categories;

/// <summary>
/// Defines the repository interface for CategoryType entity.
/// </summary>
public interface ICategoryTypeRepository : IRepository<CategoryType, Guid>
{
    /// <summary>
    /// Gets a CategoryType by its code.
    /// </summary>
    /// <param name="code">The code to search for.</param>
    /// <returns>The CategoryType if found, null otherwise.</returns>
    Task<CategoryType> GetByCodeAsync(string code);

    Task<List<CategoryType>> GetListAsync(
        string filter = "",
        bool? isActive = null,
        string sorting = nameof(CategoryType.Name),
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<bool> IsCodeExistAsync(
        string code, 
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);
} 