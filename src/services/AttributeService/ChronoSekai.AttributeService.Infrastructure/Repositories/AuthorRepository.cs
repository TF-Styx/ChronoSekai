using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Infrastructure.Repositories
{
    internal class AuthorRepository(IApplicationDbContext context) : BaseRepository<Author, IApplicationDbContext>(context), IAuthorRepository
    {
        public async Task<bool> ExistName(string name) => await _context.Authors.AnyAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
