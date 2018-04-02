using Model.DB;

namespace DAL.Interface
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<Role> RoleRepo { get; }
        void Dispose();
        int Save();
    }
}
