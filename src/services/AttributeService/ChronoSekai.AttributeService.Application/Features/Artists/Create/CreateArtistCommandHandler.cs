using AutoMapper;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Create
{
    internal class CreateArtistCommandHandler
        (
            IApplicationDbContext context,
            IArtistRepository artistRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreateArtistCommand, Result<ArtistDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IArtistRepository _artistRepository = artistRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ArtistDTO>> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<ArtistDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<ArtistDTO> validationResult))
                return validationResult;

            try
            {
                var entity = Artist.Create(request.Name);

                if (entity == null)
                    return Result<ArtistDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _artistRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<ArtistDTO>(entity);

                return Result<ArtistDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<ArtistDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
