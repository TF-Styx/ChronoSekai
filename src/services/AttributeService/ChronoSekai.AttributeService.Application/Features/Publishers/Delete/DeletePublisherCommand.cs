using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Delete
{
    public sealed record DeletePublisherCommand(int Id) : IRequest<Result>;
}
