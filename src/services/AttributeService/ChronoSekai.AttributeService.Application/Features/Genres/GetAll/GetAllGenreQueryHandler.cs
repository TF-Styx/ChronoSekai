using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Genres.GetAll
{
    public sealed class GetAllGenreQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllGenreQuery, List<GenreDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GenreDTO>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
            => await _context.Genres.AsNoTracking().ProjectTo<GenreDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
