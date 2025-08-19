using ChronoSekai.Shared.API.Application.Services.Abstraction;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoSekai.Shared.API.Application.Services.Realization
{
    public class ValidationService(IServiceProvider serviceProvider) : IValidationService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<ValidationResult> ValidateAsync<T>(T model)
        {
            if (model == null)
                return new ValidationResult([new ValidationFailure("", "Модель не может быть null")]);

            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator == null)
                return new ValidationResult();

            return await validator.ValidateAsync(model);
        }
    }
}
