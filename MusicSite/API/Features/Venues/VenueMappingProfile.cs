using AutoMapper;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Features
{
    public class VenueMappingProfile : Profile
    {
        public VenueMappingProfile()
        {
            CreateMap<VenueRequestDto, Venue>();
            CreateMap<Venue, VenueDto>();
            CreateMap<Venue, VenueDetailDto>();
        }
    }
}
