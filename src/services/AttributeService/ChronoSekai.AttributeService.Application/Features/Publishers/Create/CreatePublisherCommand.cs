using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Create
{
    public sealed record CreatePublisherCommand(string Name) : IRequest<Result<PublisherDTO>>, IHasName;
}
