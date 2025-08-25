using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.GetAll
{
    public sealed record GetAllPublisherQuery : IRequest<List<PublisherDTO>>;
}
