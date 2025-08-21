using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Delete
{
    public sealed class DeletePublisherCommandHandler
        (
            IApplicationDbContext context,
            IPublisherRepository publisherRepository,
            IValidationService validationService
        ) : IRequestHandler<DeletePublisherCommand, Result>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidationService _validationService = validationService;

        public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result validationResult))
                return validationResult;

            try
            {
                var entity = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                    return Result.Success();

                _publisherRepository.Remove(entity);
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
