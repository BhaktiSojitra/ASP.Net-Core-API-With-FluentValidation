using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class BillsValidator : AbstractValidator<BillsModel>
    {
        public BillsValidator() 
        {
            RuleFor(model => model.BillNumber)
                .NotNull().NotEmpty().WithMessage("Bill number is required.")
                .Matches("^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z0-9]*$").WithMessage("Bill number must contain at least one letter and one number, and only alphanumeric characters are allowed.")
                .MinimumLength(4).WithMessage("Bill number length should be 4 characters.")
                .MaximumLength(6).WithMessage("Bill number length should not exceed 6 characters.");

            RuleFor(model => model.OrderID)
                .GreaterThan(0).WithMessage("Order ID greater than 0 and must be a positive integer.");

            RuleFor(model => model.TotalAmount)
                //.Must(value => value is decimal).WithMessage("Total amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            RuleFor(model => model.Discount)
                //.Must(value => value is decimal).WithMessage("Discount must be of type decimal.")
                .GreaterThan(0).WithMessage("Discount must be greater than 0.");

            RuleFor(model => model.NetAmount)
                //.Must(value => value is decimal).WithMessage("Net amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Net amount must be greater than 0.");

            RuleFor(model => model.UserID)
                .GreaterThan(0).WithMessage("User ID greater than 0 and must be a positive integer.");
        }
    }
}
