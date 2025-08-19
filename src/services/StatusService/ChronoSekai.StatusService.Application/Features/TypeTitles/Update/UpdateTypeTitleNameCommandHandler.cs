using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Update
{
    internal class UpdateTypeTitleNameCommandHandler
        (
            IApplicationDbContext context,
            IValidationService validationService
        ) : IRequestHandler<UpdateTypeTitleNameCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(UpdateTypeTitleNameCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.TypeTitles.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    return Result.Failure(new Error(ErrorCode.NotFound, "Запись не найден!"));

                entity.UpdateName(request.Name);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (DomainException ex)
            {
                return Result.Failure(new Error(ErrorCode.DomainException, ex.Message));
            }
            catch (Exception)
            {
                return Result.Failure(new Error(ErrorCode.ServerError, "Критическая ошибка сервера!"));
            }
        }
    }
}
