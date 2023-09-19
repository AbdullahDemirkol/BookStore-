using Microsoft.AspNetCore.Builder;
using WebAPI.Extensions.Middlewares.Middleware;

namespace WebAPI.Extensions.Middlewares.MiddlewareExtension
{
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
