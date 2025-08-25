using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Authors.GetAll
{
    public sealed record GetAllAuthorQuery : IRequest<List<AuthorDTO>>;
}
