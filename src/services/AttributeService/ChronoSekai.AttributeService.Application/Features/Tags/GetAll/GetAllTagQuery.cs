using MediatR;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.Features.Tags.GetAll
{
    public sealed record GetAllTagQuery : IRequest<List<TagDTO>>;
}
