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
    public class UserSlotController : ApiController
    {
        private readonly IUserSlotManager userSlotService;
        // GET api/values
        public UserSlotController(IUserSlotManager chapterService)
        {
            this.userSlotService = chapterService;
        }
        [HttpGet]
        public IEnumerable<UserSlotModel> Get()
        {

            return this.userSlotService.GetUserSlot();
        }

        // GET api/values/5
        public UserSlotModel Get(int id)
        {
            return this.userSlotService.GetUserSlotById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post(UserSlotModel chapterModel)
        {
            userSlotService.CreateUserSlot(chapterModel);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]UserSlotModel model)
        {
            userSlotService.UpdateUserSlot(id, model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            userSlotService.RemoveUserSlot(id);
        }
    }
}
