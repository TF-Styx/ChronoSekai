using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Infrastructure.Repositories
{
    internal class GenreRepository(IApplicationDbContext context) : BaseRepository<Genre, IApplicationDbContext>(context), IGenreRepository
    {
        public async Task<bool> ExistName(string name) => await _context.Genres.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
