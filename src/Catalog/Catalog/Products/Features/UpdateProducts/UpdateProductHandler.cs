using Catalog.Data;
using Catalog.Products.Dtos;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catalog.Products.Features.UpdateProducts
{
    public record UpddateProductCommand(ProductDto ProductDto): ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool success);
    public class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpddateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpddateProductCommand request, CancellationToken cancellationToken)
        {
           var product = await dbContext.Products.FindAsync(new object?[] { request.ProductDto.Id }, cancellationToken);
            if (product == null)
            {
                return new UpdateProductResult(false);
            }
            product.Update(
                request.ProductDto.Name,
                request.ProductDto.Description,
                request.ProductDto.Price,
                request.ProductDto.ImageFile,
                request.ProductDto.Category,
                "system"
                );
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
