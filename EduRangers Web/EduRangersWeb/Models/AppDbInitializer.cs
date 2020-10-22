using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EduRangersWeb.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            db.Users.Add(new User { UserName = "Хидео Кодзима", Login = "genius", Password = "admin" });
            db.Users.Add(new User { UserName = "Admin", Login = "admin", Password = "admin" });
            db.Users.Add(new User { UserName = "Юрий Новиков", Login = "Arhang03", Password = "admin" });

            base.Seed(db);
        }
    }
}