using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangers.Models
{
    public class Ability
    {
        [Required]
        public int Id { get; set; }
        public string AbilityDescription { get; set; }
        public List<Course> RestrictedCourses { get; set; }
        public List<UserSlot> UserSlots { get; set; }
    }
}