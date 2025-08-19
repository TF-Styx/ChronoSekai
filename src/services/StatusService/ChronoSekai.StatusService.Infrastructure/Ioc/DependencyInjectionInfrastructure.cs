using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Infrastructure.EF.Contexts;
using ChronoSekai.StatusService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoSekai.StatusService.Infrastructure.Ioc
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StatusDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<StatusDbContext>());
            services.AddScoped<IStatusTitleRepository, StatusTitleRepository>();
            services.AddScoped<IStatusTranslateRepository, StatusTranslateRepository>();
            services.AddScoped<ITypeTitleRepository, TypeTitleRepository>();

            return services;
        }
    }
}
