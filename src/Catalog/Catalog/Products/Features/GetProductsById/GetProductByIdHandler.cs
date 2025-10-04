using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.GetProductsById
{
    public record GetProductByIdQuery(Guid ProductId) :
        IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto? Product);
    public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly Data.CatalogDbContext _catalogDbContext;
        public GetProductByIdHandler(CatalogDbContext catalogDbContext) { 

            this._catalogDbContext = catalogDbContext;
        }
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _catalogDbContext.Products
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            var productDto = products == null ? null : products.Adapt<ProductDto>();

            return products == null ? new GetProductByIdResult(null) : new GetProductByIdResult(productDto);

        }
    }
}
