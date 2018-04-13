using AutoMapper;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace WebApp.IoC
{
    public class AutoMapperProfileConfiguration : Profile
    {
         public AutoMapperProfileConfiguration()
               : this("MyProfile")
           {
               //mapping db into dto
               Mapper.Initialize(cfg =>
               {
                   cfg.CreateMap<User, UserDTO>().ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                   .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                   .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email));
                   cfg.CreateMap<List<User>, List<UserDTO>>();

                   cfg.CreateMap<TestTask, TaskDTO>().ForMember(dest => dest.ID, options => options.MapFrom(src => src.ID))
                   .ForMember(dest => dest.TaskName, options => options.MapFrom(src => src.TaskName))
                   .ForMember(dest => dest.TaskString, options => options.MapFrom(src => src.TaskString))
                   .ForMember(dest => dest.TeacherID, options => options.MapFrom(src => src.TeacherID))
                   .ForMember(dest => dest.Course, options => options.MapFrom(src => src.Course));
                   cfg.CreateMap<List<TestTask>, List<TaskDTO>>();

                   cfg.CreateMap<Course, CourseDTO>().ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Description, options => options.MapFrom(src => src.Description))
                   .ForMember(dest => dest.IsActive, options => options.MapFrom(src => src.IsActive))
                   .ForMember(dest => dest.CreationDate, options => options.MapFrom(src => src.CreationDate))
                   .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId));
                   cfg.CreateMap<List<Course>, List<CourseDTO>>();
               });
           }

           protected AutoMapperProfileConfiguration(string profileName)
               : base(profileName)
           {

           }
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
