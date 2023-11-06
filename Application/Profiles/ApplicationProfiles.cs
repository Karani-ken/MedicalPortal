using ApplicationsService.Models;
using ApplicationsService.Models.Dtos;
using AutoMapper;

namespace ApplicationsService.Profiles
{
    public class ApplicationProfiles:Profile
    {
        public ApplicationProfiles()
        {
            CreateMap<ApplicationRequestDto, Application>().ReverseMap();           
        }
    }
}
