using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Infrastructure.EF.Contexts;
using ChronoSekai.AttributeService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoSekai.AttributeService.Infrastructure.Ioc
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AttributeDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AttributeDbContext>());

            services.AddScoped<IArtistRepository,       ArtistRepository>();
            services.AddScoped<IAuthorRepository,       AuthorRepository>();
            services.AddScoped<IGenreRepository,        GenreRepository>();
            services.AddScoped<IPublisherRepository,    PublisherRepository>();
            services.AddScoped<ITagRepository,          TagRepository>();

            return services;
        }
    }
}
