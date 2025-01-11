using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator() 
        {
            RuleFor(model => model.CustomerName).NotNull()
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(model => model.HomeAddress).NotNull()
                .NotEmpty().WithMessage("Home address is required.");

            RuleFor(model => model.Email).NotNull()
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please Enter Valid Email");

            RuleFor(model => model.MobileNo).NotNull()
                .NotEmpty().WithMessage("Mobile number is required.")
                .Length(10).WithMessage("Mobile number should be 10 digit");

            RuleFor(model => model.GSTNo).NotNull()
                .NotEmpty().WithMessage("GST number is required.")
                .MaximumLength(15).WithMessage("Length should be 15.");

            RuleFor(model => model.CityName).NotNull()
                .NotEmpty().WithMessage("City name is required.");

            RuleFor(model => model.PinCode).NotNull()
                .NotEmpty().WithMessage("Pin code is required.")
                .Length(6).WithMessage("Pin code should be 6 digit");

            RuleFor(model => model.NetAmount)
                .Must(value => value is decimal).WithMessage("Net amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Net amount must be greater than 0.");

            RuleFor(model => model.UserID)
            .GreaterThan(0).WithMessage("User ID greater than 0 and must be a positive integer.");
        }
    }
}
