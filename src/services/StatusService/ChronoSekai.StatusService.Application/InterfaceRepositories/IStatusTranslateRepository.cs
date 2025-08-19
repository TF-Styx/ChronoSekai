using ChronoSekai.Shared.API.Infrastructure;
using ChronoSekai.StatusService.Domain.Models;

namespace ChronoSekai.StatusService.Application.InterfaceRepositories
{
    public interface IStatusTranslateRepository : IBaseRepository<StatusTranslate>
    {
        Task<bool> ExistName(string name);
    }
}