using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(model => model.ProductName).NotNull()
                .NotEmpty().WithMessage("Product name is required.");

            RuleFor(model => model.ProductPrice)
                //.Must(value => value is double).WithMessage("Product price must be of type double.")
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");

            RuleFor(model => model.ProductCode).NotNull()
                .NotEmpty().WithMessage("Product code is required.")
                .MaximumLength(6).WithMessage("Length should be 6.");

            RuleFor(model => model.Description).NotNull()
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(model => model.UserID)
            .GreaterThan(0).WithMessage("User ID greater than 0 and must be a positive integer.");
        }
    }
}
