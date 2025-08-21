using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Infrastructure.Repositories
{
    internal class PublisherRepository(IApplicationDbContext context) : BaseRepository<Publisher, IApplicationDbContext>(context), IPublisherRepository
    {
        public async Task<bool> ExistName(string name) => await _context.Publishers.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
