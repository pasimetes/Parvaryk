using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parvaryk.Infrastructure.Cache;

namespace Parvaryk.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheProvider, MemoryCacheProvider>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDbContext<ApplicationReadOnlyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ReadOnlyConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IApplicationReadOnlyDbContext>(provider => provider.GetService<ApplicationReadOnlyDbContext>());

            return services;
        }
    }
}
