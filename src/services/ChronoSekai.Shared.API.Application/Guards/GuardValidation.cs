using ChronoSekai.Shared.Domain.Results;
using FluentValidation.Results;

namespace ChronoSekai.Shared.API.Application.Guards
{
    public static class GuardValidation
    {
        public static bool TryGetFailureResult(ValidationResult validationResult, out Result failureResult)
        {
            if (validationResult.IsValid)
            {
                failureResult = null!;
                return false;
            }

            //$"{e.PropertyName}: {e.ErrorMessage}"

            var validationErrors = validationResult.Errors.Select(e => new Error(ErrorCode.Validation, $"{e.ErrorMessage}")).ToList();
            failureResult = Result.Failure(validationErrors);
            return true;
        }

        public static bool TryGetFailureResult<TResult>(ValidationResult validationResult, out Result<TResult> failureResult)
        {
            if (validationResult.IsValid)
            {
                failureResult = null!;
                return false;
            }

            //$"{e.PropertyName}: {e.ErrorMessage}"

            var validationErrors = validationResult.Errors.Select(e => new Error(ErrorCode.Validation, $"{e.ErrorMessage}"));
            failureResult = Result<TResult>.Failure(validationErrors);
            return true;
        }
    }
}
