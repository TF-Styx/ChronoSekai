using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Delete
{
    public sealed class DeleteTagCommandHandler
        (
            IApplicationDbContext context, 
            ITagRepository tagRepository,
            IValidationService validationService
        ) : IRequestHandler<DeleteTagCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    return Result.Success();

                _tagRepository.Remove(entity);
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
