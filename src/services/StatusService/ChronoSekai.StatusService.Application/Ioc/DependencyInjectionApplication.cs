using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.API.Application.Services.Realization;
using ChronoSekai.StatusService.Application.Features.StatusTitles.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoSekai.StatusService.Application.Ioc
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateStatusTitleCommandValidator).Assembly);

            services.AddScoped<IValidationService, ValidationService>();

            return services;
        }
    }
}
