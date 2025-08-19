using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Update
{
    public sealed record UpdateStatusTitleNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
