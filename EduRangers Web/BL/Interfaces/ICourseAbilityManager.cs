using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ICourseAbilityManager
    {
        CourseAbilityModel GetCourseAbilityById(int courseId, int abilityId);

        //CourseAbilityModel GetAbility(Func<CourseAbility, bool> predicate);

        void CreateCourseAbility(CourseAbilityModel model);

        //void UpdateAbility(int id, CourseAbilityModel model);

        void RemoveCourseAbility(int courseId, int abilityId);
    }
}
