using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Update
{
    public sealed record UpdateTagNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
