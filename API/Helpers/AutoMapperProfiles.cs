using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<VirtualBatterySystem, VirtualBatterySystemDTO>();
        }
    }
}