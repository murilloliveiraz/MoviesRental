using FluentValidation;
using MoviesRental.Core.ValidationMessages;

namespace MoviesRental.Query.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 60;
        public CreateDirectorCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.FullName)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(MIN_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
                .MaximumLength(MAX_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);
            RuleFor(d => d.CreatedAt)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.UpdatedAt)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
        }
    }
}
