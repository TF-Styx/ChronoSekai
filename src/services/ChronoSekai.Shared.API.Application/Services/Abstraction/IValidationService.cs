using FluentValidation.Results;

namespace ChronoSekai.Shared.API.Application.Services.Abstraction
{
    public interface IValidationService
    {
        Task<ValidationResult> ValidateAsync<T>(T model);
    }
}
