using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}
