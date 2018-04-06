using AutoMapper;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace WebApp.IoC
{
    public class AutoMapperProfileConfiguration : Profile
    {
      /*  public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
            //mapping db into dto
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email));
                cfg.CreateMap<List<User>, List<UserDTO>>();
            });
        }

        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {

        }*/
        //public AutoMapperProfileConfiguration()
        //    : this("MyProfile")
        //{
        //    //mapping db into dto
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<User, UserDTO>();
        //        cfg.CreateMap<List<User>, List<UserDTO>>();
        //    });
        //}
    }
}
