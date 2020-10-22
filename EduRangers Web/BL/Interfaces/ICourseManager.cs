using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ICourseManager
    {
        IEnumerable<Course> GetCourse();

        Course GetCourseById(int id);

        Course GetCourse(Func<Course, bool> predicate);

        void CreateCourse(CourseModel model);

        void UpdateCourse(int id, CourseModel model);

        void RemoveCourse(int id);
    }
}
