using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Create
{
    public sealed record CreateTagCommand(string Name) : IRequest<Result<TagDTO>>, IHasName;
}
