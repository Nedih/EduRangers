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
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        private readonly ICourseManager courseService;
        private readonly ITestManager testService;
        private readonly ICourseAbilityManager courseAbilityService;
        private readonly IAbilityManager abilityService;
        // GET api/values
        public CourseController(ICourseManager chapterService, ITestManager testService)
        {
            this.courseService = chapterService;
            this.testService = testService;
        }
        [HttpGet]
        public IEnumerable<CourseModel> Get()
        {

            return this.courseService.GetCourse();
        }

        // GET api/values/5
        public CourseModel Get(int id)
        {
            CourseModel course = this.courseService.GetCourseById(id);
            IEnumerable<TestModel> t = this.testService.GetTests(id);
            foreach (var i in t) {
                course.Tests.Add(i);
            }
            return course;
        }

        [Route("ProfCourses")]
        public IEnumerable<CourseModel> GetProfCurses(string email)
        {
            return this.courseService.GetCourseByProf(email);
        }

        // POST api/values
        [HttpPost]
        public void Post(CourseModel courseModel) //int[] abilities,
        {
            courseService.CreateCourse(courseModel);
            /*foreach(int id in abilities)
            {
                CourseAbilityModel model = new CourseAbilityModel { Course = courseModel, Ability = abilityService.GetAbilityById(id)};
                courseAbilityService.CreateCourseAbility(model);
            }*/
        }

        // PUT api/values/5
        public void Put(int id, int[] abilities, [FromBody]CourseModel model)
        {
            courseService.UpdateCourse(id, model);
            courseAbilityService.RemoveCourseAbility(model.Id);
            foreach (int abilityId in abilities)
            {               
                CourseAbilityModel courseAbility = new CourseAbilityModel { Course = model, Ability = abilityService.GetAbilityById(abilityId) };
                courseAbilityService.CreateCourseAbility(courseAbility);
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            courseService.RemoveCourse(id);
        }
    }
}
