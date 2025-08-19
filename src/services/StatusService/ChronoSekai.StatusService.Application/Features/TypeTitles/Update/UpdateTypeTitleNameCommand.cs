using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Update
{
    public sealed record UpdateTypeTitleNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
