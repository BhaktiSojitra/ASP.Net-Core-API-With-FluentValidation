﻿using API_DEMO.Models;
using FluentValidation;

namespace API_DEMO.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator() 
        {
            RuleFor(model => model.OrderNumber)
                .GreaterThan(0).WithMessage("Order number greater than 0 and must be a positive integer.");

            RuleFor(model => model.CustomerID)
                .GreaterThan(0).WithMessage("Customer ID greater than 0 and must be a positive integer.");

            RuleFor(model => model.PaymentMode)
                .NotNull().NotEmpty().WithMessage("Payment mode is required.");

            RuleFor(model => model.TotalAmount)
                //.Must(value => value is decimal).WithMessage("Total amount must be of type decimal.")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            RuleFor(model => model.ShippingAddress)
                .NotNull().NotEmpty().WithMessage("Shipping Address is required.");

            RuleFor(model => model.UserID)
                .GreaterThan(0).WithMessage("User ID greater than 0 and must be a positive integer.");
        }
    }
}
