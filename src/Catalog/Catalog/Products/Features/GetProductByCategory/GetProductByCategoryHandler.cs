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

namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) :
        IQuery<GetProductByCategoryResult>;
    
    public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);
    public class GetProductByCategoryHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await catalogDbContext.Products
                .AsNoTracking()
                .Where(p => p.Categories.Contains(request.Category))
                .ToListAsync();

            var productDto = products.Adapt<IEnumerable<ProductDto>>();
            return new GetProductByCategoryResult(productDto);
        }
    }
}
