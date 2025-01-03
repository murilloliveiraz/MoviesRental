using FluentValidation;
using MoviesRental.Core.ValidationMessages;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Application.Features.Dvds.Commands.UpdateDvd
{
    public class UpdateDvdCommandValidator : AbstractValidator<UpdateDvdCommand>
    {
        private const int GENRE_ERROR_NUMBER = 18;
        private const string GENRE_ERROR_MESSAGE = "Invalid genre type";
        private const int COPIES_ERROR_NUMBER = -1;
        public UpdateDvdCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.Title)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(Dvd.MIN_TITLE_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
                .MaximumLength(Dvd.MAX_TITLE_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);
            RuleFor(d => d.Genre)
                .LessThan(GENRE_ERROR_NUMBER).WithMessage(GENRE_ERROR_MESSAGE)
                .GreaterThan(COPIES_ERROR_NUMBER).WithMessage(GENRE_ERROR_MESSAGE);
            RuleFor(d => d.Published)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.Copies)
                .GreaterThan(COPIES_ERROR_NUMBER).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.DirectorId)
                .NotEqual(Guid.Empty).WithMessage(ValidationMessages.ERROR_MESSAGE);
        }
    }
}
