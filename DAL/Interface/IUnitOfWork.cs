using Model.DB;

namespace DAL.Interface
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<Role> RoleRepo { get; }
        IBaseRepository<TestTask> TaskRepo { get; }
        void Dispose();
        int Save();
    }
}
