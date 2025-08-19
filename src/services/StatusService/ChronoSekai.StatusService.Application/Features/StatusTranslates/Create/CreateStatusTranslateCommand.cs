using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Create
{
    public sealed record CreateStatusTranslateCommand(string Name) : IRequest<Result<StatusTranslateDTO>>, IHasName;
}
