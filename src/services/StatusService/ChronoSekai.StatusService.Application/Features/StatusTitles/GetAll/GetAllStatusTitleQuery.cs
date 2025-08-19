using MediatR;
using ChronoSekai.Shared.Contracts.StatusService;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.GetAll
{
    public sealed record GetAllStatusTitleQuery : IRequest<List<StatusTitleDTO>>;
}
