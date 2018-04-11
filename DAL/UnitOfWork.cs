using System;
using DAL.Repositories;
using Model.DB;
using DAL.Interface;
using Model;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext context;

        private IBaseRepository<Role> roleRepo;
        private IBaseRepository<User> userRepo;
        private IBaseRepository<TestTask> taskRepo;
        private IBaseRepository<Course> courseRepo;

        public UnitOfWork(MainDbContext context)
        {
            this.context = context;
        }

        public IBaseRepository<Role> RoleRepo
        {
            get
            {
                if (roleRepo == null) { roleRepo = new BaseRepository<Role>(context); }
                return roleRepo;
            }
        }

        public IBaseRepository<User> UserRepo
        {
            get
            {
                if (userRepo == null) { userRepo = new BaseRepository<User>(context); }
                return userRepo;
            }
        }

        public IBaseRepository<TestTask> TaskRepo
        {
            get
            {
                if (taskRepo == null) { taskRepo = new BaseRepository<TestTask>(context); }
                return taskRepo;
            }
        }

        public IBaseRepository<Course> CourseRepo
        {
            get
            {
                if (courseRepo == null) { courseRepo = new BaseRepository<Course>(context); }
                return courseRepo;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        private bool isDisposed = false;

        protected virtual void Grind(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Grind(true);
            GC.SuppressFinalize(this);
        }
    }
}
