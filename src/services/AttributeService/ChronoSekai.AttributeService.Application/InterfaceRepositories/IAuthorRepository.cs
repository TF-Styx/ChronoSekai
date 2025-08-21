using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;

namespace ChronoSekai.AttributeService.Application.InterfaceRepositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<bool> ExistName(string name);
    }
}