using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Genres.Update
{
    public sealed record UpdateGenreNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
