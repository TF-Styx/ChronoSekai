using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;

namespace ChronoSekai.AttributeService.Application.InterfaceRepositories
{
    public interface IArtistRepository : IBaseRepository<Artist>
    {
        Task<bool> ExistName(string name);
    }
}