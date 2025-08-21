using AutoMapper;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.AutoMapper
{
    public sealed class ArtistProfile : Profile
    {
        public ArtistProfile() => CreateMap<Artist, ArtistDTO>();
    }
}
