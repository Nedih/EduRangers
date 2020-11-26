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

            this.repository.AddAndSave(course);
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

            var mapper = MapHelper.Mapping<Course, CourseModel>();
            return mapper.Map<CourseModel>(this.repository.FirstorDefault(predicate));
        }

        public CourseModel GetCourseById(int id)
        {

            var mapper = MapHelper.Mapping<Course, CourseModel>();
            return mapper.Map<CourseModel>(this.repository.FirstorDefault<Course>(x => x.Id == id));
        }

        public IEnumerable<CourseModel> GetCourseByProf(string email)
        {
            var mapper = MapHelper.Mapping<Course, CourseModelMap>();
            var mapper2 = MapHelper.Mapping<CourseModelMap, CourseModel>();
            return mapper2.Map<List<CourseModel>>(mapper.Map<List<CourseModelMap>>(this.repository.GetCourseWhere<Course>(x => x.Author.Email == email)));
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
