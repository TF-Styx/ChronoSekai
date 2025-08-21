using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Genres.Create
{
    public sealed record CreateGenreCommand(string Name) : IRequest<Result<GenreDTO>>, IHasName;
}
