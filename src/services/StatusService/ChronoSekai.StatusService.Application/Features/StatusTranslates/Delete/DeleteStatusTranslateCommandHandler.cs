using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Delete
{
    public class DeleteStatusTranslateCommandHandler
        (
            IApplicationDbContext context,
            IStatusTranslateRepository statusTranslateRepository,
            IValidationService validationService
        ) : IRequestHandler<DeleteStatusTranslateCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IStatusTranslateRepository _statusTranslateRepository = statusTranslateRepository;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(DeleteStatusTranslateCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationRequest))
                return validationRequest;

            try
            {
                var entity = await _context.StatusTranslates.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity is null)
                    return Result.Success();

                _statusTranslateRepository.Remove(entity);
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
