using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Tags.GetAll
{
    public sealed class GetAllTagQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllTagQuery, List<TagDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<TagDTO>> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
            => await _context.Tags.AsNoTracking().ProjectTo<TagDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
