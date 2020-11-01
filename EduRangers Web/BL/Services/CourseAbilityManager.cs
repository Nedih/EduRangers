using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CourseAbilityManager : ICourseAbilityManager
    {
        private readonly IRepository repository;

        public CourseAbilityManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateCourseAbility(CourseAbilityModel model)
        {
            //Ability ability = new Ability();

            var mapper = MapHelper.Mapping<CourseAbilityModel, CourseAbility>();
            CourseAbility ability = mapper.Map<CourseAbility>(model);

            this.repository.AddAndSave<CourseAbility>(ability);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }

        public CourseAbilityModel GetCourseAbilityById(int courseId, int abilityId)
        {

            var mapper = MapHelper.Mapping<CourseAbilityModel, CourseAbility>();
            return mapper.Map<CourseAbilityModel>(this.repository.FirstorDefault<CourseAbility>(x => x.Ability.Id == abilityId && x.Course.Id == courseId));
        }

        public void RemoveCourseAbility(int courseId)
        {
            var ability = this.repository.FirstorDefault<CourseAbility>(x => x.Course.Id == courseId);
            while (ability != null)
            {
                ability = this.repository.FirstorDefault<CourseAbility>(x => x.Course.Id == courseId);
                if (ability == null)
                    throw new NullReferenceException();
                this.repository.RemoveAndSave(ability);
            }
        }
    }
}
