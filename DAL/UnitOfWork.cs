using System;
using DAL.Repositories;
using Model.DB;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MainDbContext context;

        private IBaseRepository<Role> roleRepo;
        private IBaseRepository<User> userRepo;
        private IBaseRepository<TestTask> taskRepo;

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

        public int Save()
        {
            return context.SaveChanges();
        }

        //public void UpdateContext()
        //{
        //    context = new MainDbContext();
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
