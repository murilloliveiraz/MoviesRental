﻿namespace MoviesRental.Core.ValidationMessages
{
    public static class ValidationMessages
    {
        public const string MIN_LENGTH_ERROR_MESSAGE = "{PropertyName} must have at least {MinLength} characters";
        public const string MAX_LENGTH_ERROR_MESSAGE = "{PropertyName} must not reach {MaxLength} characters";
        public const string EMPTY_STRING_ERROR_MESSAGE = "{PropertyName} can't be empty";
        public const string ERROR_MESSAGE = "Invalid {PropertyName}";
    }
}
