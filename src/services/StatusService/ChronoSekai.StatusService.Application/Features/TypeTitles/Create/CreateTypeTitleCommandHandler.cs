using AutoMapper;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.Shared.Domain.Results;
using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using ChronoSekai.StatusService.Domain.Models;
using MediatR;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Create
{
    internal class CreateTypeTitleCommandHandler
        (
            IApplicationDbContext context,
            ITypeTitleRepository typeTitleRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreateTypeTitleCommand, Result<TypeTitleDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ITypeTitleRepository _typeTitleRepository = typeTitleRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<TypeTitleDTO>> Handle(CreateTypeTitleCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<TypeTitleDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<TypeTitleDTO> validationRequest))
                return validationRequest;

            try
            {
                var entity = TypeTitle.Create(request.Name);

                if (entity is null)
                    return Result<TypeTitleDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _typeTitleRepository.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<TypeTitleDTO>(entity);

                return Result<TypeTitleDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<TypeTitleDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
