using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Delete
{
    public sealed record DeleteAuthorCommand(int Id) : IRequest<Result>;
}
