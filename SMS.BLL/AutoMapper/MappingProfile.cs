using AutoMapper;
using SMS.BLL.Models.ViewModels;
using SMS.BLL.Models.ViewModels;
using SMSCore.Models.Entities;

namespace SMS.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<University, UniversityDto>();
            CreateMap<UniversityDto, University>();
        }
    }
}
