using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Domain.Models;

namespace ChronoSekai.StatusService.Application.InterfaceRepositories
{
    public interface IStatusTitleRepository : IBaseRepository<StatusTitle>
    {
        Task<bool> ExistName(string name);
    }
}
