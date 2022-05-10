using AutoMapper;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Features
{
    public class ShowMappingProfile : Profile
    {
        public ShowMappingProfile()
        {
            CreateMap<Show, ShowSummaryDto>();
            CreateMap<Show, ShowListDto>();
            CreateMap<ShowRequestDto, Show>();
        }
    }
}
