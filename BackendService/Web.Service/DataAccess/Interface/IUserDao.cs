using Web.Service.DataAccess.Entity;

namespace Web.Service.DataAccess.Interface
{
    public interface IUserDao
    {
        IList<User> GetUsers();
    }
}
