using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.Validators
{
    public class StockDtoValidator : AbstractValidator<StockDto>
    {
        public StockDtoValidator()
        {
        }
    }
}
