using System;
using DAL.Repositories;
using Model.DB;
using System.Data.Entity.Validation;
using DAL.Interface;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MainDbContext context;

        private IBaseRepository<Role> roleRepo;
        private IBaseRepository<User> userRepo;

        public UnitOfWork()
        {
            context = new MainDbContext();
            roleRepo = new BaseRepository<Role>(context);
            userRepo = new BaseRepository<User>(context);
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

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                return 0;
            }
        }

        public void UpdateContext()
        {
            context = new MainDbContext();
        }

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
