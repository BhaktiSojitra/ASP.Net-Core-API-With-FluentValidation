using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailModel>
    {
        public OrderDetailValidator() 
        {
            RuleFor(model => model.OrderID)
                .GreaterThan(0).WithMessage("Order ID greater than 0 and must be a positive integer.");

            RuleFor(model => model.ProductID)
                .GreaterThan(0).WithMessage("Product ID greater than 0 and must be a positive integer.");

            RuleFor(model => model.Quantity)
                .GreaterThan(0).WithMessage("Quantity greater than 0 and must be a positive integer.");

            RuleFor(model => model.Amount)
                //.Must(value => value is decimal).WithMessage("Amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(model => model.TotalAmount)
                //.Must(value => value is decimal).WithMessage("Total amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            RuleFor(model => model.UserID)
                .GreaterThan(0).WithMessage("User ID greater than 0 and must be a positive integer.");
        }   
    }
}
