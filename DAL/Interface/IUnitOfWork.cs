using Model.DB;

namespace DAL.Interface
{
    interface IUnitOfWork
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<Role> RoleRepo { get; }
        void Dispose();
        int Save();
    }
}
