using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Identity;
using BL.Interfaces;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CourseManager : ICourseManager
    {
        private readonly IRepository repository;
        private readonly UserManager userManager;

        public CourseManager(IRepository repo, UserManager userManager)
        {
            this.repository = repo;
            this.userManager = userManager;
    }
        public void CreateCourse(CourseModel model)
        {
            //Course course = new Course();

            var mapper = MapHelper.Mapping<CourseModel, Course>();
            Course course = mapper.Map<Course>(model);
            Professor p = (Professor)this.userManager.FindByEmail(model.AuthorEmail);
            course.Author = p;
            var temp = model.Abilities;
            //course.Abilities
            List<CourseAbility> Flex = new List<CourseAbility>();
            foreach (var i in temp)
            {
                CourseAbility q = new CourseAbility();
                q.Ability = this.repository.FirstorDefault<Ability>(x => x.AbilityName == i.value);
                q.Course = course;
                Flex.Add(q);
            }
            this.repository.AddAndSave(course);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<CourseModel> GetCourse()
        {
            var mapper = MapHelper.Mapping<Course, CourseModel>();
            var list = mapper.Map<List<CourseModel>>(this.repository.GetAll<Course>());
            foreach (var i in list)
            {
                var temp = this.repository.GetAttemptsWhere<Attempt>(x => x.Test.Course.Id == i.Id);
                List<int> marks = new List<int>();
                foreach (var t in temp)
                {
                    marks.Add((int)t.Mark);
                }
                if (marks.Count != 0)
                    i.AvgMark = marks.Average();
                var abs = this.repository.GetCourseAbilityWhere<CourseAbility>(x => x.Course.Id == i.Id);
                List<SelectModel> Flex = new List<SelectModel>();
                foreach (var a in abs)
                {
                    SelectModel q = new SelectModel();
                    q.label = a.Ability.AbilityName;
                    q.value = a.Ability.AbilityName;
                    Flex.Add(q);
                }
                i.Abilities = Flex;
            }
            return list;
        }

        public CourseModel GetCourse(Func<Course, bool> predicate)
        {

            var mapper = MapHelper.Mapping<Course, CourseModel>();
            var course = mapper.Map<CourseModel>(this.repository.FirstorDefault(predicate));
            var temp = this.repository.GetAttemptsWhere<Attempt>(x => x.Test.Course.Id == course.Id);
            List<int> marks = new List<int>();
            foreach (var t in temp)
            {
                marks.Add((int)t.Mark);
            }
            if (marks.Count != 0)
                course.AvgMark = marks.Average();
            var abs = this.repository.GetCourseAbilityWhere<CourseAbility>(x => x.Course.Id == course.Id);
            List<SelectModel> Flex = new List<SelectModel>();
            foreach (var i in abs)
            {
                SelectModel q = new SelectModel();
                q.label = i.Ability.AbilityName;
                q.value = i.Ability.AbilityName;
                Flex.Add(q);
            }
            course.Abilities = Flex;
            return course;
        }

        public CourseModel GetCourseById(int id)
        {

            var mapper = MapHelper.Mapping<Course, CourseModel>();
            var course = mapper.Map<CourseModel>(this.repository.FirstorDefault<Course>(x => x.Id == id));
            var temp = this.repository.GetAttemptsWhere<Attempt>(x => x.Test.Course.Id == id);
            List<int> marks = new List<int>();
            foreach (var t in temp)
            {
                marks.Add((int)t.Mark);
            }
            if (marks.Count != 0)
                course.AvgMark = marks.Average();
            var abs = this.repository.GetCourseAbilityWhere<CourseAbility>(x => x.Course.Id == course.Id);
            List<SelectModel> Flex = new List<SelectModel>();
            foreach (var i in abs)
            {
                SelectModel q = new SelectModel();
                q.label = i.Ability.AbilityName;
                q.value = i.Ability.AbilityName;
                Flex.Add(q);
            }
            course.Abilities = Flex;
            return course;
        }

        public IEnumerable<CourseModel> GetCourseByProf(string email)
        {
            var mapper = MapHelper.Mapping<Course, CourseModelMap>();
            var mapper2 = MapHelper.Mapping<CourseModelMap, CourseModel>();
            var list = mapper2.Map<List<CourseModel>>(mapper.Map<List<CourseModelMap>>(this.repository.GetCourseWhere<Course>(x => x.Author.Email == email)));
            
            foreach (var i in list)
            {
                var temp = this.repository.GetAttemptsWhere<Attempt>(x => x.Test.Course.Id == i.Id);
                List<int> marks = new List<int>();
                foreach (var t in temp)
                {
                    marks.Add((int)t.Mark);
                }
                if (marks.Count != 0)
                    i.AvgMark = marks.Average();
            }
            return list;
        }

        public void RemoveCourse(int id)
        {
            var course = this.repository.FirstorDefault<Course>(x => x.Id == id);
            if (course == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(course);
        }

        public void UpdateCourse(int id, CourseModel model)
        {
            var course = this.repository.FirstorDefault<Course>(x => x.Id == id);
            if (course == null)
                throw new NullReferenceException();
            var mapper = MapHelper.Mapping<CourseModel, Course>();
            course = mapper.Map<Course>(model);

            this.repository.UpdateAndSave(course);
        }
    }
}
