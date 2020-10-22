using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserSlotManager
    {
        IEnumerable<UserSlot> GetUserSlot();

        UserSlot GetUserSlotById(int id);

        UserSlot GetUserSlot(Func<UserSlot, bool> predicate);

        void CreateUserSlot(UserSlotModel model);

        void UpdateUserSlot(int id, UserSlotModel model);

        void RemoveUserSlot(int id);
    }
}
