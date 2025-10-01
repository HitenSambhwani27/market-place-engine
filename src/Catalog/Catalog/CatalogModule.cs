using Catalog.Data;
using Catalog.Data.Seed;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Interceptors;
using Shared.Seed;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CatalogModule).Assembly);
               
            });

            serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEnitityInterceptors>();
            serviceCollection.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            serviceCollection.AddDbContext<CatalogDbContext>((service, options) =>
            {
                options.AddInterceptors(service.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

              

            serviceCollection.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return serviceCollection;
        }
        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            app.UseMigration<CatalogDbContext>();
            return app;
        }

    }
   
    
}
