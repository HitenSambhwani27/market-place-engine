using Catalog.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) :
        ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool Success);
    public class DeleteProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await catalogDbContext.Products.FindAsync(new object?[] { request.ProductId }, cancellationToken);
            if (product == null)
            {
                return new DeleteProductResult(false);
            }
            catalogDbContext.Products.Remove(product);
            await catalogDbContext.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
