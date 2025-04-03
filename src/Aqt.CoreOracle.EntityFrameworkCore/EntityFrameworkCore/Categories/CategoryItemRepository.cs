using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aqt.CoreOracle.Categories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace Aqt.CoreOracle.EntityFrameworkCore.Categories;

public class CategoryItemRepository : EfCoreRepository<CoreOracleDbContext, CategoryItem, Guid>, ICategoryItemRepository
{
    public CategoryItemRepository(IDbContextProvider<CoreOracleDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async Task<CategoryItem> GetByCodeAsync(Guid categoryTypeId, string code)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<CategoryItem>().AsQueryable();

        var entity = await query
            .Include(x => x.CategoryType)
            .FirstOrDefaultAsync(x => x.CategoryTypeId == categoryTypeId && x.Code == code);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(CategoryItem));
        }

        return entity;
    }

    public async Task<List<CategoryItem>> GetListAsync(
        Guid? parentId = null,
        Guid? categoryTypeId = null,
        string? filter = null,
        bool? isActive = null,
        string? sorting = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<CategoryItem>().AsQueryable();

        if (includeDetails)
        {
            query = query.Include(x => x.CategoryType);
        }

        query = query
            .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
            .WhereIf(categoryTypeId.HasValue, x => x.CategoryTypeId == categoryTypeId)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x =>
                x.Name.Contains(filter) ||
                x.Code.Contains(filter) ||
                x.Description.Contains(filter))
            .WhereIf(isActive.HasValue, x => x.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(sorting))
        {
            query = query.OrderBy(sorting);
        }
        else
        {
            query = query.OrderBy(x => x.Code);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<CategoryItem>> GetListByTypeCodeAsync(
        string categoryTypeCode,
        Guid? parentId = null,
        bool? isActive = null,
        string? sorting = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<CategoryItem>()
            .Join(
                dbContext.Set<CategoryType>(),
                item => item.CategoryTypeId,
                type => type.Id,
                (item, type) => new { Item = item, Type = type })
            .Where(x => x.Type.Code == categoryTypeCode)
            .Select(x => x.Item);

        if (includeDetails)
        {
            query = query.Include(x => x.CategoryType);
        }

        query = query
            .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
            .WhereIf(isActive.HasValue, x => x.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(sorting))
        {
            query = query.OrderBy(sorting);
        }
        else
        {
            query = query.OrderBy(x => x.Code);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> IsCodeExistAsync(
        Guid categoryTypeId,
        string code,
        Guid? expectedId = null,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<CategoryItem>()
            .AnyAsync(x => 
                x.CategoryTypeId == categoryTypeId && 
                x.Code == code && 
                x.Id != expectedId, 
                cancellationToken);
    }
} 