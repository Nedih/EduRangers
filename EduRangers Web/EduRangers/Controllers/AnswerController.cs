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
    [RoutePrefix("api/Answer")]
    public class AnswerController : ApiController
    {
        private readonly IAnswerManager abilityService;
        // GET api/values
        public AnswerController(IAnswerManager chapterService)
        {
            this.abilityService = chapterService;
        }
        [HttpGet]
        public IEnumerable<AnswerModel> Get()
        {

            return this.abilityService.GetAnswer();
        }

        // GET api/values/5
        public AnswerModel Get(int id)
        {
            return this.abilityService.GetAnswerById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] AnswerModel chapterModel)
        {
            abilityService.CreateAnswer(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] AnswerModel model)
        {
            abilityService.UpdateAnswer(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            abilityService.RemoveAnswer(id);
        }
    }
}
