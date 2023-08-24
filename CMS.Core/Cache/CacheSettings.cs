namespace CMS.Core.Cache
{
    public class CacheSettings
    {
        /// <summary>
        /// Represent the default cache time in minutes
        /// </summary>
        public int DefaultCacheTime { get; set; }

        /// <summary>
        /// Represent whether the cache is enabled or disabled
        /// </summary>
        public bool EnableCaching { get; set; }
    }
}
