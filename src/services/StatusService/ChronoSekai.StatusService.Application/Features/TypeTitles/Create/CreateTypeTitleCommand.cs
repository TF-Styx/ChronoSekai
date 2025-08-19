using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Create
{
    public sealed record CreateTypeTitleCommand(string Name) : IRequest<Result<TypeTitleDTO>>, IHasName;
}
