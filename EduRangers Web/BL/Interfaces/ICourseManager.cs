﻿using BinderLayer.Models;
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
        IEnumerable<CourseModel> GetCourse();

        CourseModel GetCourseById(int id);

        CourseModel GetCourse(Func<Course, bool> predicate);
        IEnumerable<CourseModel> GetCourseByProf(string email);

        void CreateCourse(CourseModel model);

        void UpdateCourse(int id, CourseModel model);

        void RemoveCourse(int id);
    }
}
