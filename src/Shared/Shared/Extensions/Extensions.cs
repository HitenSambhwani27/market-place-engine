using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration<T>(this IApplicationBuilder app) where T : DbContext
        {
            InitializeDatabaseAsync<T>(app).GetAwaiter().GetResult();
            SeedDataAsync(app).GetAwaiter().GetResult();
            return app;
        }
        private static async Task InitializeDatabaseAsync<T>(IApplicationBuilder app) where T : DbContext
        {
           using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            await dbContext.Database.MigrateAsync();
        }
        private static async Task SeedDataAsync(IApplicationBuilder app) 
        {
            using var scope = app.ApplicationServices.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
           foreach(var seeds in seeders)
            {
                await seeds.SeedAllAsync();
            }
           

        }
    }
}
