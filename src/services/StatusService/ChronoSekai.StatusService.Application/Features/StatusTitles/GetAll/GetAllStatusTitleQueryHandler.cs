using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.StatusService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.GetAll
{
    public class GetAllStatusTitleQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllStatusTitleQuery, List<StatusTitleDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<StatusTitleDTO>> Handle(GetAllStatusTitleQuery request, CancellationToken cancellationToken)
            => await _context.StatusTitles.AsNoTracking().ProjectTo<StatusTitleDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
