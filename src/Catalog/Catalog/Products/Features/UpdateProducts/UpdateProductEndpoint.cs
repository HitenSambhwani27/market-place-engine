using Carter;
using Catalog.Products.Dtos;
using Catalog.Products.Features.CreateProduct;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.UpdateProducts
{
    public record UpdateProductCommand(ProductDto ProductDto);
    public record UpdateProductResponse(bool success);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/productUpdate/{id}", async (UpddateProductCommand request, ISender sender) =>
            {
                var command = request.Adapt<UpddateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Created($"/products/{response.success}", response);

            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update the product")
            .WithDescription("Updates the product with desired details.");

        }
    }
}
