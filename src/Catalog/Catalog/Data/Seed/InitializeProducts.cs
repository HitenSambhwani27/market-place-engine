using Catalog.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Seed
{
    public  class InitializeProducts
    {
        public static IEnumerable<Product> products => [
            Product.Create( "iPhone", "Iphone x", 40000, "Iphone image", new List<string>(){"Mobile","Phone"}, "" ),
            Product.Create( "iPhone", "Iphone 11", 50000, "Iphone image", new List<string>(){"Mobile","Phone"}, "" ),
            Product.Create( "iPhone", "Iphone 12", 60000, "Iphone image", new List<string>(){"Mobile","Phone"}, "" ),
            Product.Create( "iPhone", "Iphone 13", 60000, "Iphone image", new List<string>(){"Mobile","Phone"}, "" ),
            Product.Create( "iPhone", "Iphone 14", 90000, "Iphone image", new List<string>(){"Mobile","Phone"}, "" ),

            ];
    }
}
