using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Parvaryk.WebApi.Events;

namespace Parvaryk.WebApi.Configuration
{
    public static class AuthetnicationConfig
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddScoped<CustomCookieAuthenticationEvents>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                });

            return services;
        }
    }
}
