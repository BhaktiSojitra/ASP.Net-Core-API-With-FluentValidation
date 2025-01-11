using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator() 
        {
            RuleFor(model => model.UserName).NotNull()
                .NotEmpty().WithMessage("Product name is required.");

            RuleFor(model => model.Email).NotNull()
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please Enter Valid Email");

            RuleFor(model => model.Password).NotNull()
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 50).WithMessage("Password must be between 6 and 50 Characters.")
                .Matches("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$").WithMessage("Password can" +
                "only contain alphanumeric characters.");

            RuleFor(model => model.MobileNo).NotNull()
                .NotEmpty().WithMessage("Mobile number is required.")
                .Length(10).WithMessage("Mobile number should be 10 digit");

            RuleFor(model => model.Address).NotNull()
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
