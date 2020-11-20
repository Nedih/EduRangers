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
    [RoutePrefix("api/Attempt")]
    public class AttemptController : ApiController
    {
        private readonly IAttemptManager attemptService;
        // GET api/values
        public AttemptController(IAttemptManager chapterService)
        {
            this.attemptService = chapterService;
        }
        [HttpGet]
        public IEnumerable<AttemptModel> Get()
        {

            return this.attemptService.GetAttempt();
        }

        // GET api/values/5
        public AttemptModel Get(int id)
        {
            return this.attemptService.GetAttemptById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post(AttemptModel chapterModel)
        {
            attemptService.CreateAttempt(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]AttemptModel model)
        {
            attemptService.UpdateAttempt(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            attemptService.RemoveAttempt(id);
        }
    }
}
