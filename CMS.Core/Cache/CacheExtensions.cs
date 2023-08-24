using Microsoft.Extensions.DependencyInjection;

namespace CMS.Core.Cache
{
    /// <summary>
    /// Cache Extensions
    /// </summary>
    public static class CacheExtensions
    {
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services, CacheSettings settings)
        {
            services.AddSingleton(settings);

            services.AddScoped<ICacheManager, MemoryCacheService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
