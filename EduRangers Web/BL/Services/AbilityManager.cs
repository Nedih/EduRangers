using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AbilityManager : IAbilityManager
    {
        private readonly IRepository repository;

        public AbilityManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateAbility(AbilityModel model)
        {
            //Ability ability = new Ability();

            var mapper = MapHelper.Mapping<AbilityModel, Ability>();
            Ability ability = mapper.Map<Ability>(model);

            this.repository.AddAndSave<Ability>(ability);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<AbilityModel> GetAbility()
        {
            var mapper = MapHelper.Mapping<Ability, AbilityModel>();
            return mapper.Map<List<AbilityModel>>(this.repository.GetAll<Ability>());
            
        }

        public AbilityModel GetAbility(Func<Ability, bool> predicate)
        {
            
            var mapper = MapHelper.Mapping<Ability, AbilityModel>();
            return mapper.Map<AbilityModel>(this.repository.FirstorDefault(predicate));
        }

        public AbilityModel GetAbilityById(int id)
        {
            
            var mapper = MapHelper.Mapping<Ability, AbilityModel>();
            return mapper.Map<AbilityModel>(this.repository.FirstorDefault<Ability>(x => x.Id == id));
        }

        public void RemoveAbility(int id)
        {
            var ability = this.repository.FirstorDefault<Ability>(x => x.Id == id);
            if (ability == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(ability);
        }

        public void UpdateAbility(int id, AbilityModel model)
        {
            var ability = this.repository.FirstorDefault<Ability>(x => x.Id == id);
            if (ability == null)
                throw new NullReferenceException();
            //var mapper = MapHelper.Mapping<AbilityModel, Ability>();
            //ability = mapper.Map<Ability>(model);
            ability.Id = id;
            if (model.AbilityName != null)
                ability.AbilityName = model.AbilityName;
            if (model.AbilityDescription != null)
                ability.AbilityDescription = model.AbilityDescription;

            this.repository.UpdateAndSave(ability);
        }

    }
}
