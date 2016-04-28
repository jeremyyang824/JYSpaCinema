using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using JYSpaCinema.Service.DTO;

namespace JYSpaCinema.Service.Validators
{
    public class MovieDtoValidator : AbstractValidator<MovieDto>
    {
        public MovieDtoValidator()
        {
            RuleFor(movie => movie.GenreId).GreaterThan(0)
                .WithMessage("Select a Genre");

            RuleFor(movie => movie.Director).NotEmpty().Length(1, 100)
                .WithMessage("Select a Director");

            RuleFor(movie => movie.Writer).NotEmpty().Length(1, 50)
                .WithMessage("Select a writer");

            RuleFor(movie => movie.Producer).NotEmpty().Length(1, 50)
                .WithMessage("Select a producer");

            RuleFor(movie => movie.Description).NotEmpty()
                .WithMessage("Select a description");

            RuleFor(movie => movie.Rating).InclusiveBetween((byte)0, (byte)5)
                .WithMessage("Rating must be less than or equal to 5");

            RuleFor(movie => movie.TrailerURI).NotEmpty().Must(ValidTrailerURI)
                .WithMessage("Only Youtube Trailers are supported");
        }

        private bool ValidTrailerURI(string trailerURI)
        {
            return (!string.IsNullOrEmpty(trailerURI) && trailerURI.ToLower().StartsWith("http://"));
        }
    }
}
