using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess.Interface;

namespace Web.Service.DataAccess.Implement
{
    public class UserDao : IUserDao
    {
        private IdentityServerUserDbContext dataContext;

        public UserDao(IdentityServerUserDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IList<User> GetUsers()
        {
            List<User>? users = dataContext.Users?.ToList();
            return users ?? new List<User>();
        }
    }
}
