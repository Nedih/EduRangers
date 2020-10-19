using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangers.Models
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        public Professor Author { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public List<Test> Tests { get; set; }
        public List<Ability> BannedAbilities { get; set; }
    }
}