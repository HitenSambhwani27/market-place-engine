using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
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

    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductDto.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(x => x.ProductDto.Description)
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");
            RuleFor(x => x.ProductDto.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(x => x.ProductDto.ImageFile)
                .NotEmpty().WithMessage("Product image is required.");
            RuleFor(x => x.ProductDto.Category)
                .NotEmpty().WithMessage("At least one category is required.");
        }
    }
    public class ProductHandler(CatalogDbContext catalogDbContext, IValidator<CreateProductCommand> validator, ILogger<ProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // validation part

            var result = await validator.ValidateAsync(command, cancellationToken); 
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }

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
