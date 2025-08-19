using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Delete
{
    public sealed class DeleteStatusTitleCommandHandler
        (
            IValidationService validationService,
            IStatusTitleRepository statusTitleRepository,
            IApplicationDbContext context
        ) : IRequestHandler<DeleteStatusTitleCommand, Result>
    {
        private readonly IValidationService _validationService = validationService;
        private readonly IStatusTitleRepository _statusTitleRepository = statusTitleRepository;
        private readonly IApplicationDbContext _context = context;

        public async Task<Result> Handle(DeleteStatusTitleCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.StatusTitles.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity is null)
                    return Result.Success();

                _statusTitleRepository.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(new Error(ErrorCode.ServerError, "При удалении произошла ошибка на сервере!"));
            }
        }
    }
}
