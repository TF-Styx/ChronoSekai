using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.Shared.Contracts.StatusService;
using ChronoSekai.StatusService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.GetAll
{
    public class GetAllStatusTranslateQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllStatusTranslateQuery, List<StatusTranslateDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<StatusTranslateDTO>> Handle(GetAllStatusTranslateQuery request, CancellationToken cancellationToken)
            => await _context.StatusTranslates.AsNoTracking().ProjectTo<StatusTranslateDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
