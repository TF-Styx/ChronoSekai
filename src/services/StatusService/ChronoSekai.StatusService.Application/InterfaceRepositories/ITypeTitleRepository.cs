using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Domain.Models;

namespace ChronoSekai.StatusService.Application.InterfaceRepositories
{
    public interface ITypeTitleRepository : IBaseRepository<TypeTitle>
    {
        Task<bool> ExistName(string name);
    }
}