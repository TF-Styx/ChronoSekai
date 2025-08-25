using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.GetAll
{
    public sealed class GetAllPublisherQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllPublisherQuery, List<PublisherDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PublisherDTO>> Handle(GetAllPublisherQuery request, CancellationToken cancellationToken)
            => await _context.Publishers.AsNoTracking().ProjectTo<PublisherDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
