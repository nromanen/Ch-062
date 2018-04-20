using AutoMapper;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace BAL.IoC
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

                   cfg.CreateMap<Exercise, ExerciseDTO>().ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                   .ForMember(dest => dest.TaskName, options => options.MapFrom(src => src.TaskName))
                   .ForMember(dest => dest.TaskString, options => options.MapFrom(src => src.TaskString))
                   .ForMember(dest => dest.TeacherId, options => options.MapFrom(src => src.TeacherId))
                   .ForMember(dest => dest.Course, options => options.MapFrom(src => src.Course))
                   .ForMember(dest => dest.IsDeleted, options => options.MapFrom(src => src.IsDeleted))
                   .ForMember(dest => dest.CreateDateTime, options => options.MapFrom(src => src.CreateDateTime))
                   .ForMember(dest => dest.UpdateDateTime, options => options.MapFrom(src => src.UpdateDateTime));
                   cfg.CreateMap<List<Exercise>, List<ExerciseDTO>>();

                   //Course => CourseDTO
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
    }
}
