using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Delete
{
    public class DeleteTypeTitleCommandHandler
        (
            IApplicationDbContext context, 
            ITypeTitleRepository typeTitleRepository, 
            IValidationService validationService
        ) : IRequestHandler<DeleteTypeTitleCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ITypeTitleRepository _typeTitleRepository = typeTitleRepository;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(DeleteTypeTitleCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.TypeTitles.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity is null)
                    return Result.Success();

                _typeTitleRepository.Remove(entity);
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
