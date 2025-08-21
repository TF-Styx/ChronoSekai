using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Infrastructure;

namespace ChronoSekai.AttributeService.Application.InterfaceRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<bool> ExistName(string name);
    }
}