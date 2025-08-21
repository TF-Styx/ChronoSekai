using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Delete
{
    public sealed class DeleteAuthorCommandHandler
        (
            IApplicationDbContext context,
            IAuthorRepository authorRepository,
            IValidationService validationService
        ) : IRequestHandler<DeleteAuthorCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    return Result.Success();

                _authorRepository.Remove(entity);
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
