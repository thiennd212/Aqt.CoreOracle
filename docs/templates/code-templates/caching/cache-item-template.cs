using System;
using Volo.Abp.Caching;

namespace Aqt.CoreOracle.[ModuleName]
{
    [CacheName("[ModuleName]:[EntityName]")]
    public class [EntityName]CacheItem
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModificationTime { get; set; }
        
        public static string CalculateCacheKey(Guid id)
        {
            return $"[ModuleName]:[EntityName]:{id}";
        }
        
        public static string CalculateListCacheKey(string filter = null)
        {
            return $"[ModuleName]:[EntityName]:List:{filter ?? "all"}";
        }
    }
} 