using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Infrastructure.Repositories
{
    internal class ArtistRepository(IApplicationDbContext context) : BaseRepository<Artist, IApplicationDbContext>(context), IArtistRepository
    {
        public async Task<bool> ExistName(string name) => await _context.Artists.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
