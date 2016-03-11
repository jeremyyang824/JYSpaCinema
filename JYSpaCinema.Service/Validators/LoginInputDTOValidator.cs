using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.Validators
{
    public class LoginInputDtoValidator : AbstractValidator<LoginInputDto>
    {
        public LoginInputDtoValidator()
        {
            this.RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            this.RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");
        }
    }
}
