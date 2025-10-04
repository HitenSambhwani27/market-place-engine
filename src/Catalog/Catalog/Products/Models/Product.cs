using Catalog.Products.Events;
using Microsoft.AspNetCore.Http;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string Image { get; set; } = default!;
        public List<string> Categories { get; set; } = new();


        public static Product Create(string name, string description, decimal price, string image, List<string> categories, string createdBy)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegative(price);
            var product =  new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                Image = image,
                Categories = categories,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                LastModifiedBy = createdBy
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            return product;
        }
        public void Update(string name, string description, decimal price, string image, List<string> categories, string createdBy)
        {
            Name = name;
            Description = description;
            Price = price;
            Image = image;

            AddDomainEvent(new ProductPriceChangedEvent(this));

        }
    }

}
