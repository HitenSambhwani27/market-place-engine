using Carter;
using Catalog.Products.Dtos;
using Catalog.Products.Features.DeleteProduct;
using Catalog.Products.Features.GetProductByCategory;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.GetProducts
{
    //public record GetProductRequest();
    public record GetProductResponse(IEnumerable<ProductDto> ProductDtos);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            })
            .WithName("Get Product")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get product")
            .WithDescription("Get product");
        }
    }
}
