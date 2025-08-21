using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Genres.Delete
{
    public sealed record DeleteGenreCommand(int Id) : IRequest<Result>;
}
