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

public class CategoryTypeRepository : EfCoreRepository<CoreOracleDbContext, CategoryType, Guid>, ICategoryTypeRepository
{
    public CategoryTypeRepository(IDbContextProvider<CoreOracleDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async Task<CategoryType> GetByCodeAsync(string code)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<CategoryType>().AsQueryable();

        query = query.Include(x => x.Items);

        var entity = await query
            .FirstOrDefaultAsync(x => x.Code == code);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(CategoryType));
        }

        return entity;
    }

    public async Task<List<CategoryType>> GetListAsync(
        string? filter = null,
        bool? isActive = null,
        string? sorting = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var query = dbContext.Set<CategoryType>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(x => 
                x.Name.Contains(filter) || 
                x.Code.Contains(filter) ||
                x.Description.Contains(filter));
        }

        if (isActive.HasValue)
        {
            query = query.Where(x => x.IsActive == isActive.Value);
        }

        if (includeDetails)
        {
            query = query.Include(x => x.Items);
        }

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
        string code,
        Guid? expectedId = null,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<CategoryType>()
            .AnyAsync(x => x.Code == code && x.Id != expectedId, cancellationToken);
    }
} 