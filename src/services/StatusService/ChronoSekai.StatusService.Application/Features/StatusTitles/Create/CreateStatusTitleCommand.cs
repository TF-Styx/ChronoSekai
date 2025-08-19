using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Create
{
    public sealed record CreateStatusTitleCommand(string Name) : IRequest<Result<StatusTitleDTO>>, IHasName;
}
