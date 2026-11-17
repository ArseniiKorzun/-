using FluentValidation;
using WebApplication1.Contracts;

namespace WebApplication1.Validations
{
    public abstract class MovieSessionRequestValidator<T> : AbstractValidator<T> where T : IMovieSessionRequest
    {
        protected MovieSessionRequestValidator()
        {
            RuleFor(x => x.MovieId)
                .GreaterThan(0).WithMessage("Movie id must be a positive number.");

            RuleFor(x => x.StartsAt)
                .Must(d => d.Kind == DateTimeKind.Utc)
                .WithMessage("Start time must be in UTC with the 'Z' suffix, e.g. 2030-01-15T18:00:00Z.");

            RuleFor(x => x.HallName)
                .NotEmpty().WithMessage("Hall name is required.")
                .MaximumLength(50).WithMessage("Hall name cannot exceed 50 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
                .LessThanOrEqualTo(99999999.99m).WithMessage("Price cannot exceed 99999999.99.");

            RuleFor(x => x.AvailableSeats)
                .GreaterThanOrEqualTo(0).WithMessage("Available seats cannot be negative.");
        }
    }
}
