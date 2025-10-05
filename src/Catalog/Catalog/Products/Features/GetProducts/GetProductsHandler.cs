using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(): IQuery<GetProductsCommandResponse>;

    public record GetProductsCommandResponse(IEnumerable<ProductDto> ProductDtosList);
    public class GetProductsHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductsQuery, GetProductsCommandResponse>
    {
        public async Task<GetProductsCommandResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
           var productsList = await catalogDbContext.Products.AsNoTracking().ToListAsync(cancellationToken);

            var products = productsList.Adapt<ProductDto>();
            return new GetProductsCommandResponse(new List<ProductDto> { products });

        }
    }
}
