using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Create
{
    public sealed record CreateArtistCommand(string Name) : IRequest<Result<ArtistDTO>>, IHasName;
}
