using FluentValidation.Results;

namespace SmartCharging.Core.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationFailureModel> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> errors) : base("Validation Exception")
    {
        Errors = errors.Select(x => new ValidationFailureModel
        {
            Field = x.PropertyName,
            Errors = new[]
            {
                x.ErrorMessage
            }
        });
    }

    public ValidationException(IEnumerable<ValidationFailureModel> errors) : base("Validation Exception")
    {
        Errors = errors;
    }

    public ValidationException(string message, IEnumerable<ValidationFailureModel> errors) : base(message)
    {
        Errors = errors;
    }
}