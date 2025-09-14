using Catalog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            serviceCollection.AddDbContext<CatalogDbContext>(
                options => options.UseNpgsql(connectionString));

            return serviceCollection;
        }
        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            InitializeDatabaseAsync(app).GetAwaiter().GetResult();
            return app;
        }

        private static async Task  InitializeDatabaseAsync(IApplicationBuilder app)
        {
           var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            await dbContext.Database.MigrateAsync ();
        }
    }
   
    
}
