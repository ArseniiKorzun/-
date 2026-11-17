using FluentValidation;
using WebApplication1.Contracts;

namespace WebApplication1.Validations
{
    public class CreateMovieSessionRequestValidator : MovieSessionRequestValidator<CreateMovieSessionRequest>
    {
        public CreateMovieSessionRequestValidator()
        {
            RuleFor(x => x.StartsAt)
                .Must(d => d > DateTime.UtcNow).WithMessage("Session cannot be created in the past.")
                .When(x => x.StartsAt.Kind == DateTimeKind.Utc);
        }
    }
}
