using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangers.Models
{
    public class UserSlot
    {
        [Required]
        public int Id { get; set; }
        public Student Owner { get; set; }
        public Ability Ability { get; set; }
        public int count { get; set; }
    }
}