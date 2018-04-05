using AutoMapper;
using Model.DB;
using Model.DTO;

namespace WebApp.IoC
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {
            //mapping db into dto
            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PasswordHash, options => options.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email));
        }
    }
}
