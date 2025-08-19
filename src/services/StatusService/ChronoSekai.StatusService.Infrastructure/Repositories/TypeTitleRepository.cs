using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Infrastructure.Repositories
{
    internal class TypeTitleRepository(IApplicationDbContext context) : BaseRepository<TypeTitle, IApplicationDbContext>(context), ITypeTitleRepository
    {
        public async Task<bool> ExistName(string name) => await _context.TypeTitles.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
