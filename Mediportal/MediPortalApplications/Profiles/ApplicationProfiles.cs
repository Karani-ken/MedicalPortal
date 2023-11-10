using MediPortalApplications.Models;
using MediPortalApplications.Models.Dtos;
using AutoMapper;

namespace MediPortalApplications.Profiles
{
    public class ApplicationProfiles:Profile
    {
        public ApplicationProfiles()
        {
            CreateMap<ApplicationRequestDto, Application>().ReverseMap();           
        }
    }
}
