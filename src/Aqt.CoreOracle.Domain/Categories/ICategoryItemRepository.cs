using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Categories;

/// <summary>
/// Defines the repository interface for CategoryItem entity.
/// </summary>
public interface ICategoryItemRepository : IRepository<CategoryItem, Guid>
{
    /// <summary>
    /// Gets a CategoryItem by its code within a specific CategoryType.
    /// </summary>
    /// <param name="categoryTypeId">The ID of the CategoryType.</param>
    /// <param name="code">The code to search for.</param>
    /// <returns>The CategoryItem if found, null otherwise.</returns>
    Task<CategoryItem> GetByCodeAsync(Guid categoryTypeId, string code);

    Task<List<CategoryItem>> GetListAsync(
        Guid? parentId = null,
        Guid? categoryTypeId = null,
        string? filter = null,
        bool? isActive = null,
        string? sorting = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<List<CategoryItem>> GetListByTypeCodeAsync(
        string categoryTypeCode,
        Guid? parentId = null,
        bool? isActive = null,
        string? sorting = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<bool> IsCodeExistAsync(
        Guid categoryTypeId,
        string code,
        Guid? expectedId = null,
        CancellationToken cancellationToken = default);
} 