using AutoMapper;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Create
{
    public sealed class CreatePublisherCommandHandler
        (
            IApplicationDbContext context,
            IPublisherRepository publisherRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreatePublisherCommand, Result<PublisherDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<PublisherDTO>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<PublisherDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<PublisherDTO> validationResult))
                return validationResult;

            try
            {
                var entity = Publisher.Create(request.Name);

                if (entity == null)
                    return Result<PublisherDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _publisherRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<PublisherDTO>(entity);

                return Result<PublisherDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<PublisherDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
