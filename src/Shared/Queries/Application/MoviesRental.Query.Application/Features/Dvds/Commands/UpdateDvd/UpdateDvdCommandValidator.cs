﻿using FluentValidation;
using MoviesRental.Core.ValidationMessages;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.UpdateDvd
{
    public class UpdateDvdCommandValidator : AbstractValidator<UpdateDvdCommand>
    {
        public UpdateDvdCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.Title)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);
            RuleFor(d => d.Published)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.Genre)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);
            RuleFor(d => d.Copies)
                .GreaterThan(-1).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.UpdatedAt)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(d => d.DirectorId)
                .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);
        }
    }
}
