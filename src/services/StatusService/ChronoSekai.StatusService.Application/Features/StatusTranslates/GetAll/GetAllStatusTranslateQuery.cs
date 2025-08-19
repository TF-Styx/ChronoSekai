using ChronoSekai.Shared.Contracts.StatusService;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.GetAll
{
    public sealed record GetAllStatusTranslateQuery : IRequest<List<StatusTranslateDTO>>;
}
