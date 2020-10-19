using EduRangers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EduRangers.Controllers
{
    public class ValuesController : ApiController
    {
        ApplicationContext db = new ApplicationContext();

        public IEnumerable<User> GetUsers()
        {
            return db.Users;
        }

        public User GetUser(int id)
        {
            User User = db.Users.Find(id);
            return User;
        }

        [HttpPost]
        public void CreateUser([FromBody]User User)
        {
            db.Users.Add(User);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditUser(int id, [FromBody]User User)
        {
            if (id == User.Id)
            {
                db.Entry(User).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            User User = db.Users.Find(id);
            if (User != null)
            {
                db.Users.Remove(User);
                db.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
