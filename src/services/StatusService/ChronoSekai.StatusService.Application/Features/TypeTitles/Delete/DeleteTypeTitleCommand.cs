using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Delete
{
    public sealed record DeleteTypeTitleCommand(int Id) : IRequest<Result>;
}
