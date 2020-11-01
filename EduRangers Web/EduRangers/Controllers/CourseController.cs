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
        private readonly ICourseAbilityManager courseAbilityService;
        private readonly IAbilityManager abilityService;
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
        public void Post(int[] abilities, CourseModel courseModel)
        {
            courseService.CreateCourse(courseModel);
            foreach(int id in abilities)
            {
                CourseAbilityModel model = new CourseAbilityModel { Course = courseModel, Ability = abilityService.GetAbilityById(id)};
                courseAbilityService.CreateCourseAbility(model);
            }
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
