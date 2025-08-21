using AutoMapper;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.API.Application.Guards;
using ChronoSekai.Shared.API.Application.Services.Abstraction;
using ChronoSekai.Shared.Contracts.AttributeService;
using ChronoSekai.Shared.Domain.Results;
using MediatR;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Create
{
    public sealed class CreateAuthorCommandHandler
        (
            IApplicationDbContext context,
            IAuthorRepository authorRepository,
            IValidationService validationService,
            IMapper mapper
        ) : IRequestHandler<CreateAuthorCommand, Result<AuthorDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IValidationService _validationService = validationService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AuthorDTO>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (GuardRequest.TryGetFailureResult(request, out Result<AuthorDTO> requestResult))
                return requestResult;

            if (GuardValidation.TryGetFailureResult(await _validationService.ValidateAsync(request), out Result<AuthorDTO> validationResult))
                return validationResult;

            try
            {
                var entity = Author.Create(request.Name);

                if (entity == null)
                    return Result<AuthorDTO>.Failure(new Error(ErrorCode.Empty, "Не удалось!"));

                await _authorRepository.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<AuthorDTO>(entity);

                return Result<AuthorDTO>.Success(dto);
            }
            catch (Exception)
            {
                return Result<AuthorDTO>.Failure(new Error(ErrorCode.ServerError, "При сохранении произошла ошибка на сервере!"));
            }
        }
    }
}
