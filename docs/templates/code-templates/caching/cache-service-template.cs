using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Aqt.CoreOracle.[ModuleName]
{
    public class [EntityName]CacheService : ITransientDependency
    {
        private readonly IDistributedCache<[EntityName]CacheItem> _cache;
        private readonly IDistributedCache<List<[EntityName]CacheItem>> _listCache;
        private readonly I[EntityName]Repository _repository;
        private readonly IObjectMapper _objectMapper;

        public [EntityName]CacheService(
            IDistributedCache<[EntityName]CacheItem> cache,
            IDistributedCache<List<[EntityName]CacheItem>> listCache,
            I[EntityName]Repository repository,
            IObjectMapper objectMapper)
        {
            _cache = cache;
            _listCache = listCache;
            _repository = repository;
            _objectMapper = objectMapper;
        }

        public async Task<[EntityName]CacheItem> GetAsync(Guid id)
        {
            return await _cache.GetOrAddAsync(
                [EntityName]CacheItem.CalculateCacheKey(id),
                async () =>
                {
                    var entity = await _repository.GetAsync(id);
                    return _objectMapper.Map<[EntityName], [EntityName]CacheItem>(entity);
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
                }
            );
        }

        public async Task<List<[EntityName]CacheItem>> GetListAsync(string filter = null)
        {
            return await _listCache.GetOrAddAsync(
                [EntityName]CacheItem.CalculateListCacheKey(filter),
                async () =>
                {
                    var entities = await _repository.GetListAsync();
                    return _objectMapper.Map<List<[EntityName]>, List<[EntityName]CacheItem>>(entities);
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
                }
            );
        }

        public async Task InvalidateAsync(Guid id)
        {
            await _cache.RemoveAsync([EntityName]CacheItem.CalculateCacheKey(id));
        }

        public async Task InvalidateListAsync()
        {
            await _listCache.RemoveAsync([EntityName]CacheItem.CalculateListCacheKey());
        }
    }
} 