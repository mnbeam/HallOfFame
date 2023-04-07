using FluentValidation.Results;

namespace HallOfFame.Core.Exceptions;

public class HallOfFameValidationException : Exception
{
    public HallOfFameValidationException()
        : base("One or more validation failures have occured")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public HallOfFameValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public HallOfFameValidationException(params string[]? errors) : this()
    {
        if (errors != null)
        {
            Errors.Add(string.Empty, errors);
        }
    }

    public IDictionary<string, string[]> Errors { get; }
}