using AutoMapper;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Create
{
    public sealed class CreateTagCommandHandler
        (
            IApplicationDbContext context,
            ITagRepository tagRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreateTagCommand, Result<TagDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<TagDTO>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<TagDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<TagDTO> validationResult))
                return validationResult;

            try
            {
                var entity = Tag.Create(request.Name);

                if (entity == null)
                    return Result<TagDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _tagRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<TagDTO>(entity);

                return Result<TagDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<TagDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
