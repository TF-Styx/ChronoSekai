using AutoMapper;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Create
{
    internal class CreateStatusTitleCommandHandler
        (
            IValidationService validationService, 
            IStatusTitleRepository statusTitleRepository,
            IApplicationDbContext context,
            IMapper mapper
        ) : IRequestHandler<CreateStatusTitleCommand, Result<StatusTitleDTO>>
    {
        private readonly IValidationService _validationService = validationService;
        private readonly IStatusTitleRepository _statusTitleRepository = statusTitleRepository;
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<StatusTitleDTO>> Handle(CreateStatusTitleCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<StatusTitleDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<StatusTitleDTO> validationRequest))
                return validationRequest;

            try
            {
                var domain = StatusTitle.Create(request.Name);

                // TODO : Подумать над названием и типом возвращаемого значения
                if (GuardRequest.TryGetFailureResult(domain, out Result<StatusTitleDTO> domainResult))
                    return domainResult;

                await _statusTitleRepository.AddAsync(domain);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<StatusTitleDTO>(domain);

                return Result<StatusTitleDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<StatusTitleDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
