using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();

            // ileride redis 'e geicilirse MemoryCacheManager degistirilir.
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            // business kısmında web temelli olarak user claim bilgilerine erismeyi saglamak icin !
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<Stopwatch>();
        }
    }
}
