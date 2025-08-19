using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Delete
{
    public sealed record DeleteStatusTranslateCommand(int Id) : IRequest<Result>;
}
