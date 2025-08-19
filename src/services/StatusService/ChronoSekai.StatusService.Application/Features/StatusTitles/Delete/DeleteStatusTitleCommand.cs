using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Delete
{
    public sealed record DeleteStatusTitleCommand(int Id) : IRequest<Result>;
}
