using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EduRangersWeb.Models;

namespace EduRangersWeb.Controllers
{
    public class CoursesController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        public IEnumerable<Course> GetCourses()
        {
            return db.Courses;
        }

        public Course GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            return course;
        }

        [HttpPost]
        public void CreateCourse([FromBody]Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditCourse(int id, [FromBody]Course course)
        {
            if (id == course.Id)
            {
                db.Entry(course).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}