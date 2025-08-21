using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Create
{
    public sealed record CreateAuthorCommand(string Name) : IRequest<Result<AuthorDTO>>, IHasName;
}
