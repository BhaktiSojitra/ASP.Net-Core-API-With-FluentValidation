using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator() 
        {
            foreach (var property in typeof(UserModel).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var propertyName = property.Name;
                    RuleFor(x => property.GetValue(x) as string)
                        .Must(value => value != "string")
                        .WithMessage($"{propertyName} cannot have the value 'string'.");
                }
            }

            RuleFor(model => model.UserName)
                .NotNull().NotEmpty().WithMessage("UserName name is required.")
                .MinimumLength(10).WithMessage("UserName must be at least 10 characters long.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("UserName must not contain special characters. " +
                        "Only letters, numbers, and underscores are allowed.");

            RuleFor(model => model.Email)
                .NotNull().NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please Enter Valid Email");

            RuleFor(model => model.Password)
                .NotNull().NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(model => model.MobileNo)
                .NotNull().NotEmpty().WithMessage("Mobile number is required.")
                .Length(10).WithMessage("Mobile number should be 10 digit");

            RuleFor(model => model.Address)
                .NotNull().NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");
        }
    }
}
