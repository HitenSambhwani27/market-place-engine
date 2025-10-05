using Catalog.Data;
using Catalog.Products.Dtos;
using FluentValidation;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.UpdateProducts
{
    public record UpdateProductCommand(ProductDto ProductDto) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool success);
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductDto.Id)
                .NotEmpty().WithMessage("Product ID is required.");
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
        public class UpdateProductHandler(CatalogDbContext dbContext, IValidator<UpdateProductCommand> validator) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
        {
            public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                if (errors.Any())
                {
                    throw new FluentValidation.ValidationException(errors.FirstOrDefault());
                }

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
}
