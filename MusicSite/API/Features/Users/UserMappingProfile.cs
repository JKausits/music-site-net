using AutoMapper;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Features
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterDto, User>()
                .ForMember(x => x.Password, opt => opt.MapFrom(x => BCrypt.Net.BCrypt.HashPassword(x.Password)))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email.ToLower()));
            CreateMap<User, UserTokenDto>();
        }
    }
}
