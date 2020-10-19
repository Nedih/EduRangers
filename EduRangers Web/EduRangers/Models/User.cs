using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduRangers.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
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