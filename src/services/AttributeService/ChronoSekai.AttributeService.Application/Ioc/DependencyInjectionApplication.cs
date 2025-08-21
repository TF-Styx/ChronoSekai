using ChronoSekai.AttributeService.Application.Features.Artists.Create;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.API.Application.Services.Realization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoSekai.AttributeService.Application.Ioc
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateArtistCommandValidator).Assembly);

            services.AddScoped<IValidationService, ValidationService>();

            return services;
        }
    }
}
