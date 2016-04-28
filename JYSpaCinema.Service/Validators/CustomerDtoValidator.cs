using System;
using FluentValidation;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty()
                .Length(1, 100).WithMessage("First Name must be between 1 - 100 characters");

            RuleFor(customer => customer.LastName).NotEmpty()
                .Length(1, 100).WithMessage("Last Name must be between 1 - 100 characters");

            RuleFor(customer => customer.IdentityCard).NotEmpty()
                .Length(1, 100).WithMessage("Identity Card must be between 1 - 50 characters");

            RuleFor(customer => customer.DateOfBirth).NotNull()
                .LessThan(DateTime.Now.AddYears(-16))
                .WithMessage("Customer must be at least 16 years old.");

            RuleFor(customer => customer.Mobile).NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Mobile phone must have 10 digits");

            RuleFor(customer => customer.Email).NotEmpty().EmailAddress()
                .WithMessage("Enter a valid Email address");
        }
    }
}
