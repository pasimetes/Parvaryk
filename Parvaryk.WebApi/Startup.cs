using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Parvaryk.Application;
using Parvaryk.Contracts;
using Parvaryk.Infrastructure;
using Parvaryk.WebApi.Configuration;
using Parvaryk.WebApi.Middleware;
using Parvaryk.WebApi.Services;

namespace Parvaryk.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers();

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.ConfigureAuthentication();
            services.ConfigureSwagger();
            services.ConfigureLogging(Configuration);
            services.AddHealthChecks();

            services.AddScoped<IUserDetails, UserDetails>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseCustomExceptionHandler(logger);
                app.UseHsts();
            }

            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));
            app.UseMvc();
        }
    }
}
