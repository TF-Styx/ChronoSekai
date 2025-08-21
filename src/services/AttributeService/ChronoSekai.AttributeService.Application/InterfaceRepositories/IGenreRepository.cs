using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;

namespace ChronoSekai.AttributeService.Application.InterfaceRepositories
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<bool> ExistName(string name);
    }
}