using Microsoft.EntityFrameworkCore;
using Shared.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Seed
{
    public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if (! await  dbContext.Products.AnyAsync())
            {
                var products = new List<Products.Models.Product>
                {
                    new Products.Models.Product
                    {
                        Name = "Product 1",
                        Description = "Description for Product 1",
                        Price = 10.99m,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Products.Models.Product
                    {
                        Name = "Product 2",
                        Description = "Description for Product 2",
                        Price = 15.49m,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Products.Models.Product
                    {
                        Name = "Product 3",
                        Description = "Description for Product 2",
                        Price = 15.49m,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Products.Models.Product
                    {
                        Name = "Product 4",
                        Description = "Description for Product 2",
                        Price = 15.49m,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Products.Models.Product
                    {
                        Name = "Product 5",
                        Description = "Description for Product 3",
                        Price = 7.99m,
                        CreatedAt = DateTime.UtcNow
                    }
                };
                await dbContext.Products.AddRangeAsync(products);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
