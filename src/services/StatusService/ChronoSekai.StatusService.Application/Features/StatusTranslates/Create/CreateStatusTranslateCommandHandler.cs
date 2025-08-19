using AutoMapper;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Create
{
    public class CreateStatusTranslateCommandHandler
        (
            IApplicationDbContext context, 
            IStatusTranslateRepository statusTranslateRepository, 
            IValidationService validationService, 
            IMapper mapper
        ) : IRequestHandler<CreateStatusTranslateCommand, Result<StatusTranslateDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IStatusTranslateRepository _statusTranslateRepository = statusTranslateRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<StatusTranslateDTO>> Handle(CreateStatusTranslateCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<StatusTranslateDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<StatusTranslateDTO> validationResult))
                return validationResult;

            try
            {
                var entity = StatusTranslate.Create(request.Name);

                if (entity == null)
                    return Result<StatusTranslateDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _statusTranslateRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<StatusTranslateDTO>(entity);

                return Result<StatusTranslateDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<StatusTranslateDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
