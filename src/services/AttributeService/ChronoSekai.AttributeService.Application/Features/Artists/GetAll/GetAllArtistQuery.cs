using MediatR;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.Features.Artists.GetAll
{
    public sealed record GetAllArtistQuery : IRequest<List<ArtistDTO>>;
}
