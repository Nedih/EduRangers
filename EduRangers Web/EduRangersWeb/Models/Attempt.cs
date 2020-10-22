using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangersWeb.Models
{
    public class Attempt
    {
        [Required]
        public int Id { get; set; }
        public double Mark { get; set; } 
        public bool Result { get; set; }
        public Student Student { get; set; }
        public Test Test { get; set; }
    }
}