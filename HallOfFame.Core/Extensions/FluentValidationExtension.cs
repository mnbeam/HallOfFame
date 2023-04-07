using FluentValidation;
using HallOfFame.Core.Exceptions;

namespace HallOfFame.Core.Extensions;

public static class FluentValidationExtensions
{
    public static void ValidateAndThrowIfInvalid<T>(this AbstractValidator<T> validator, T value)
    {
        var validationResult = validator.Validate(value);

        if (!validationResult.IsValid)
        {
            throw new HallOfFameValidationException(validationResult.Errors);
        }
    }
}