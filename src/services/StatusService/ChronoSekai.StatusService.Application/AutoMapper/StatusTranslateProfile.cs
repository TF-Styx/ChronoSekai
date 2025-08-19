using AutoMapper;
using ChronoSekai.StatusService.Domain.Models;
using ChronoSekai.Shared.Contracts.StatusService;

namespace ChronoSekai.StatusService.Application.AutoMapper
{
    public sealed class StatusTranslateProfile : Profile
    {
        public StatusTranslateProfile() => CreateMap<StatusTranslate, StatusTranslateDTO>();
    }
}
