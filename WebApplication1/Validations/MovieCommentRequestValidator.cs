using FluentValidation;
using WebApplication1.Contracts;

namespace WebApplication1.Validations
{
    public abstract class MovieCommentRequestValidator<T> : AbstractValidator<T> where T : IMovieCommentRequest
    {
        protected MovieCommentRequestValidator()
        {
            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters.");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Comment text is required.")
                .MaximumLength(1000).WithMessage("Comment text cannot exceed 1000 characters.");
        }
    }
}
