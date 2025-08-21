using MediatR;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.Features.Genres.GetAll
{
    public sealed record GetAllGenreQuery : IRequest<List<GenreDTO>>;
}
