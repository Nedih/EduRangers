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
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private readonly ITestManager testService;
        private readonly IQuestionManager questionService;
        // GET api/values
        public TestController(ITestManager chapterService, IQuestionManager questionService)
        {
            this.testService = chapterService;
            this.questionService = questionService;
        }
        [HttpGet]
        public IEnumerable<TestModel> Get()
        {

            return this.testService.GetTest();
        }

        // GET api/values/5
        [Route("Mark")]
        [HttpGet]
        public double Mark(int id)
        {
            return this.testService.AvgMark(id);
        }

        public TestModel GetTest(int id)
        {
            TestModel test = this.testService.GetTestById(id);
            foreach (var i in this.questionService.GetQuestions(id))
            {
                test.Questions.Add(i);
            }
            return test;
        }

        // POST api/values
        [HttpPost]
        public void Post(TestModel chapterModel)
        {
            testService.CreateTest(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]TestModel model)
        {
            testService.UpdateTest(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            testService.RemoveTest(id);
        }
    }
}
