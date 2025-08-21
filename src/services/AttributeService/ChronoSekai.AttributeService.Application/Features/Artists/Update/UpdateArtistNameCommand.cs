using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Update
{
    public sealed record UpdateArtistNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
