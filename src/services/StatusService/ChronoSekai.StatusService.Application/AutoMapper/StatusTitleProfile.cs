using AutoMapper;
using ChronoSekai.StatusService.Domain.Models;
using ChronoSekai.Shared.Contracts.StatusService;

namespace ChronoSekai.StatusService.Application.AutoMapper
{
    public sealed class StatusTitleProfile : Profile
    {
        public StatusTitleProfile() => CreateMap<StatusTitle, StatusTitleDTO>();
    }
}
