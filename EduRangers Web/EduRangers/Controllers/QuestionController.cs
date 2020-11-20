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
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        private readonly IQuestionManager questionService;
        // GET api/values
        public QuestionController(IQuestionManager chapterService)
        {
            this.questionService = chapterService;
        }
        [HttpGet]
        public IEnumerable<QuestionModel> Get()
        {

            return this.questionService.GetQuestion();
        }

        // GET api/values/5
        public QuestionModel Get(int id)
        {
            return this.questionService.GetQuestionById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post(QuestionModel chapterModel)
        {
            questionService.CreateQuestion(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]QuestionModel model)
        {
            questionService.UpdateQuestion(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            questionService.RemoveQuestion(id);
        }
    }
}
