using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Common
{
    public interface IApplicationDbContext : IBaseContext
    {
        DbSet<Artist> Artists { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        DbSet<Tag> Tags { get; set; }
    }
}
