using Model.DB;
using System;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<Role> RoleRepo { get; }
        IBaseRepository<TestTask> TaskRepo { get; }
        IBaseRepository<Course> CourseRepo { get; }
        int Save();
    }
}
