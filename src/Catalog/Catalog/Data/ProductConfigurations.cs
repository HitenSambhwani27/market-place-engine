using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Name).IsRequired().HasMaxLength(100);
            builder.Property(I => I.Description).HasMaxLength(500);
            builder.Property(I => I.Price).IsRequired();
            builder.Property(I => I.Image).HasMaxLength(500);
            builder.Property(I => I.Categories).IsRequired();
        }
    }
}
