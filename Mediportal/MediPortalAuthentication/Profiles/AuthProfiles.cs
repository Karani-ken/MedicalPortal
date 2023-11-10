using AutoMapper;
using MediPortalAuthentication.Model.Dtos;
using MediPortalAuthentication.Model;

namespace MediPortalAuthentication.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterRequestDto, User>()
          .ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));

            CreateMap<User, UserDto>().ReverseMap();
        }
      
    }
}
