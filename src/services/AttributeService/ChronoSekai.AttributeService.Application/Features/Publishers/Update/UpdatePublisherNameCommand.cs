using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Update
{
    public sealed record UpdatePublisherNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
