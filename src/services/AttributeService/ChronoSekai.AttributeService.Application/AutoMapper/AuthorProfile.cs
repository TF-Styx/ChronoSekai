using AutoMapper;
using ChronoSekai.AttributeService.Domain.Models;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace ChronoSekai.AttributeService.Application.AutoMapper
{
    public sealed class AuthorProfile : Profile
    {
        public AuthorProfile() => CreateMap<Author, AuthorDTO>();
    }
}
