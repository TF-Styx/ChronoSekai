using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Artists.GetAll
{
    public sealed class GetAllArtistQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllArtistQuery, List<ArtistDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<ArtistDTO>> Handle(GetAllArtistQuery request, CancellationToken cancellationToken)
            => await _context.Artists.AsNoTracking().ProjectTo<ArtistDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
