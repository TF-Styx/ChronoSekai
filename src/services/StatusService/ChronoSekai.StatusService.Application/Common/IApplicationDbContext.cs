using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Common
{
    public interface IApplicationDbContext : IBaseContext
    {
        DbSet<StatusTitle> StatusTitles { get; set; }
        DbSet<StatusTranslate> StatusTranslates { get; set; }
        DbSet<TypeTitle> TypeTitles { get; set; }
    }
}
