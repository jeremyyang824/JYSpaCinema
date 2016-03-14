using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.Validators
{
    public class RegistrationInputDtoValidator : AbstractValidator<RegistrationInputDto>
    {
        public RegistrationInputDtoValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            RuleFor(r => r.Password).NotEmpty()
                .WithMessage("Invalid password");
        }
    }
}
