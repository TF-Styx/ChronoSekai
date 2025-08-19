using ChronoSekai.Shared.Contracts.StatusService;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.GetAll
{
    public sealed record GetAllTypeTitleQuery : IRequest<List<TypeTitleDTO>>;
}
