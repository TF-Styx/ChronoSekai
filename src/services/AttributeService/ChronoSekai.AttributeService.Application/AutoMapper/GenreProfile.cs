using AutoMapper;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.AutoMapper
{
    public sealed class GenreProfile : Profile
    {
        public GenreProfile() => CreateMap<Genre, GenreDTO>();
    }
}
