using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.StatusService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.GetAll
{
    internal class GetAllTypeTitleQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllTypeTitleQuery, List<TypeTitleDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<TypeTitleDTO>> Handle(GetAllTypeTitleQuery request, CancellationToken cancellationToken)
            => await _context.TypeTitles.AsNoTracking().ProjectTo<TypeTitleDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
