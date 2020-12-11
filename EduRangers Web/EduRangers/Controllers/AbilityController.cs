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
    [RoutePrefix("api/Ability")]
    public class AbilityController : ApiController
    {
        private readonly IAbilityManager abilityService;
        // GET api/values
        public AbilityController(IAbilityManager chapterService)
        {
            this.abilityService = chapterService;
        }
        [HttpGet]
        public IEnumerable<AbilityModel> Get()
        {
               
            return this.abilityService.GetAbility();
        }

        [Route("Abs")]
        [HttpGet]
        public IEnumerable<SelectModel> Abs()
        {
            var abs = this.abilityService.GetAbility();
            List<SelectModel> Flex = new List<SelectModel>();
            foreach (var i in abs)
            {
                SelectModel q = new SelectModel();
                q.label = i.AbilityName;
                q.value = i.AbilityName;
                Flex.Add(q);
            }
            return Flex;
        }

        // GET api/values/5
        public AbilityModel Get(int id)
        {
            return this.abilityService.GetAbilityById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]AbilityModel chapterModel)
        {
            abilityService.CreateAbility(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]AbilityModel model)
        {
            abilityService.UpdateAbility(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            abilityService.RemoveAbility(id);
        }
    }
}
