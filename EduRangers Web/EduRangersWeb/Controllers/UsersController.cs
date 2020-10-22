using EduRangersWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EduRangersWeb.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        public IEnumerable<User> GetUsers()
        {
            return db.Users;
        }

        public User GetUser(int id)
        {
            User user = db.Users.Find(id);
            return user;
        }

        [HttpPost]
        public void CreateUser([FromBody]User user)
        {
            if (db.Users.FirstOrDefault(x => x.Login == user.Login) == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        [HttpPut]
        public void EditUser(int id, [FromBody]User user)
        {
            if (id == user.Id)
            {
                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
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
