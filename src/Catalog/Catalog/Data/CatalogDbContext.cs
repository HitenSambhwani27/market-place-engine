using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class CatalogDbContext: DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext>  options): base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("catalog");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);


            var entries  = builder.Model.GetEntityTypes().Where(t => typeof(ISoftDeletable).IsAssignableFrom(t.ClrType));

            foreach(var entityType in entries)
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(e => !EF.Property<bool>(e, "IsDeleted"));
            }
        }

    }
    public class testkrle
    {
        public async Task test(CatalogDbContext dbContext, string name)
        {
            int maxtries = 0;
            int limit = 3;

            try
            {
                using var transaction = dbContext.Database.BeginTransaction();
                var products = dbContext.Products.Where(p => p.Price > 100).ToList();

                foreach (var product in products)
                {
                    product.Name = name;
                }
                dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is NpgsqlException sqlEx)
            {
                // Handle deadlock
                if(maxtries > limit)
                {
                    throw;
                }
                Task.Delay(1000 * (int)Math.Pow(2, maxtries)).Wait();
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
