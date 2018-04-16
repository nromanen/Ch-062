using Model.DB;
using System;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<IdentityRole> RoleRepo { get; }
        IBaseRepository<Exercise> ExerciseRepo { get; }
        IBaseRepository<Course> CourseRepo { get; }
        int Save();
    }
}
