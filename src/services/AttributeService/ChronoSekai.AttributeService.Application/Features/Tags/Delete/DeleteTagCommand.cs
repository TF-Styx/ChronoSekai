using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Delete
{
    public sealed record DeleteTagCommand(int Id) : IRequest<Result>;
}
