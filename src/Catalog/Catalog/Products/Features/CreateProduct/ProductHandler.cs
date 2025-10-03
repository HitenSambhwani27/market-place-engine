using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using MediatR;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto ProductDto) :
        ICommand<CreateProductResult>;

    public record CreateProductResult(Guid id);
    public class ProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = CreateNewProduct(command.ProductDto);

            catalogDbContext.Products.Add(product);
            await catalogDbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto productDto)
        {
            var product = Product.Create(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.ImageFile,
                productDto.Category,
                "system"
                );
            return product;
        }
    }
}
