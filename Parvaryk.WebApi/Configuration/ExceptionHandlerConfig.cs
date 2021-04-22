using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Parvaryk.WebApi.Configuration
{
    public static class ExceptionHandlerConfig
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    logger.LogError(exceptionHandlerPathFeature?.Error.ToString());
                    await context.Response.WriteAsync($"An error has occured in Parvaryk.WebApi - {exceptionHandlerPathFeature?.Error.Message}");
                });
            });
        }
    }
}
