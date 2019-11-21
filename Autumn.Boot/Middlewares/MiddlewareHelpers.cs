using Autumn.AuthHelper;
using Microsoft.AspNetCore.Builder;

namespace Autumn.Middlewares
{
    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }

        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMiddle>();
        }
    }
}
