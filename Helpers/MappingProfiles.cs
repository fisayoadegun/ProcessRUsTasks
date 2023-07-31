using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProcessRUsTasks.Dtos;
using ProcessRUsTasks.Models;

namespace ProcessRUsTasks.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, RegisterDto>()
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        }
    }
}
