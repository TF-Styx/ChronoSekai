using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Update
{
    public sealed record UpdateStatusTranslateNameCommand(int Id, string Name) : IRequest<Result>, IHasName;
}
