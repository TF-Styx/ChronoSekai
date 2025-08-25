using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.Shared.Contracts.AttributeService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChronoSekai.AttributeService.Application.Features.Authors.GetAll
{
    public sealed class GetAllAuthorQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllAuthorQuery, List<AuthorDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<AuthorDTO>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
            => await _context.Authors.AsNoTracking().ProjectTo<AuthorDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
