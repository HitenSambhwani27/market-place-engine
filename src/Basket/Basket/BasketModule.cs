using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection applicationService, IConfiguration configuration)
        {
            return applicationService;
        }
        public static IApplicationBuilder UseBasketgModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
