using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Parvaryk.WebApi.Configuration
{
    public static class LoggingConfig
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.AddConsole();
                loggerBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggerBuilder.AddProvider(new Log4NetProvider(configuration.GetValue<string>("Log4NetConfig")));
            });

            return services;
        }
    }
}
