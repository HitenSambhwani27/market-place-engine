﻿using Catalog.Products.Models;
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
            if (!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(InitializeProducts.products);
                await dbContext.SaveChangesAsync();
            }
            
        }
    }
}
