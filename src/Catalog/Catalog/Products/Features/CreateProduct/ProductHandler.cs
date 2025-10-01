using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> categories, string Description, decimal Price) :
        IRequest<CreateProductResult>;

    public record CreateProductResult(Guid id);
    public class ProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
