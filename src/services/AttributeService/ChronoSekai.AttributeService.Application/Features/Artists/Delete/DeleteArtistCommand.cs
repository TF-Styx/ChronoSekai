using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Delete
{
    public sealed record DeleteArtistCommand(int Id) : IRequest<Result>;
}
