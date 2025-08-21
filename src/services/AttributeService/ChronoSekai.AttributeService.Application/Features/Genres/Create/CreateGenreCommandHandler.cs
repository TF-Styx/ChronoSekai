using AutoMapper;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Genres.Create
{
    public sealed class CreateGenreCommandHandler
        (
            IApplicationDbContext context,
            IGenreRepository genreRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreateGenreCommand, Result<GenreDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<GenreDTO>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<GenreDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<GenreDTO> validationResult))
                return validationResult;

            try
            {
                var entity = Genre.Create(request.Name);

                if (entity == null)
                    return Result<GenreDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _genreRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<GenreDTO>(entity);

                return Result<GenreDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<GenreDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
