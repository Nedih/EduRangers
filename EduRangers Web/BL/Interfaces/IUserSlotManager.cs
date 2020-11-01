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
        IEnumerable<UserSlotModel> GetUserSlot();

        UserSlotModel GetUserSlotById(int id);

        UserSlotModel GetUserSlot(Func<UserSlot, bool> predicate);

        void CreateUserSlot(UserSlotModel model);

        void UpdateUserSlot(int id, UserSlotModel model);

        void RemoveUserSlot(int id);
        void UseAbility(int abilityId, string studentId);
    }
}
