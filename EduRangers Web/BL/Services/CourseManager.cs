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
    public class CourseManager : ICourseManager
    {
        private readonly IRepository repository;
        private readonly ICourseAbilityManager courseAbilityService;

        public CourseManager(IRepository repo, ICourseAbilityManager courseAbilityService)
        {
            this.repository = repo;
            this.courseAbilityService = courseAbilityService;
    }
        public void CreateCourse(CourseModel model)
        {
            //Course course = new Course();

            var mapper = MapHelper.Mapping<CourseModel, Course>();
            Course course = mapper.Map<Course>(model);

            this.repository.AddAndSave<Course>(course);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<CourseModel> GetCourse()
        {
            var mapper = MapHelper.Mapping<Course, CourseModel>();
            return mapper.Map<List<CourseModel>>(this.repository.GetAll<Course>());

        }

        public CourseModel GetCourse(Func<Course, bool> predicate)
        {

            var mapper = MapHelper.Mapping<CourseModel, Course>();
            return mapper.Map<CourseModel>(this.repository.FirstorDefault(predicate));
        }

        public CourseModel GetCourseById(int id)
        {

            var mapper = MapHelper.Mapping<CourseModel, Course>();
            return mapper.Map<CourseModel>(this.repository.FirstorDefault<Course>(x => x.Id == id));
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
            var mapper = MapHelper.Mapping<Course, CourseModel>();
            course = mapper.Map<Course>(model);

            this.repository.UpdateAndSave(course);
        }
    }
}
