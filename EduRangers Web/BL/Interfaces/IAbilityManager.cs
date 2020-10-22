using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAbilityManager
    {
        IEnumerable<AbilityModel> GetAbility();

        AbilityModel GetAbilityById(int id);

        AbilityModel GetAbility(Func<Ability, bool> predicate);

        void CreateAbility(AbilityModel model);

        void UpdateAbility(int id, AbilityModel model);

        void RemoveAbility(int id);
    }
}
