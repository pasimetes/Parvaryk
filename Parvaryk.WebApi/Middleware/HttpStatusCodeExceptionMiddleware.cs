using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Parvaryk.Domain.Exceptions;

namespace Parvaryk.WebApi.Middleware
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var serializedError = string.Empty;

            var code = exception switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                InvalidCredentialsException _ => HttpStatusCode.Unauthorized,
                ConflictException _ => HttpStatusCode.Conflict,
                _ => throw exception,
            };

            if (string.IsNullOrEmpty(serializedError))
            {
                serializedError = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(serializedError);
        }
    }
}
