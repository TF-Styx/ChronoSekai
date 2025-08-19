using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Infrastructure.Repositories
{
    internal class StatusTitleRepository(IApplicationDbContext context) : BaseRepository<StatusTitle, IApplicationDbContext>(context), IStatusTitleRepository
    {
        public async Task<bool> ExistName(string name) => await _context.StatusTitles.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
