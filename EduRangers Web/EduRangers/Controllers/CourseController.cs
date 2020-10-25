using BinderLayer.Models;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EduRangers.Controllers
{
    [EnableCors(origins: "http://localhost:3000/", headers: "", methods: "")]
    public class CourseController : ApiController
    {
        private readonly ICourseManager courseService;
        // GET api/values
        public CourseController(ICourseManager chapterService)
        {
            this.courseService = chapterService;
        }
        [HttpGet]
        public IEnumerable<CourseModel> Get()
        {

            return this.courseService.GetCourse();
        }

        // GET api/values/5
        public CourseModel Get(int id)
        {
            return this.courseService.GetCourseById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post(CourseModel chapterModel)
        {
            courseService.CreateCourse(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]CourseModel model)
        {
            courseService.UpdateCourse(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            courseService.RemoveCourse(id);
        }
    }
}
