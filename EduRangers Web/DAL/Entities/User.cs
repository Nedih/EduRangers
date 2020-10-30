using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        //public string Login { get; set; }
        //public string Password { get; set; }
        public string UserAvatar { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }
    }

    public class Student : User
    {
        public string Group;
        public List<Course> StudentCourses { get; set; }
        public List<UserSlot> Slots { get; set; }
        public List<Attempt> Attempts { get; set; }
    }

    public class Professor : User
    {
        public bool IsApplied { get; set; }
        public List<Course> ProfessorCourses { get; set; }
    }
}