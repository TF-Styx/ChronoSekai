using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Infrastructure.Repositories
{
    internal class StatusTranslateRepository(IApplicationDbContext context) : BaseRepository<StatusTranslate, IApplicationDbContext>(context), IStatusTranslateRepository
    {
        public async Task<bool> ExistName(string name) => await _context.StatusTranslates.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
