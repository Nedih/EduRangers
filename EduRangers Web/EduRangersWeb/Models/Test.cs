using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangersWeb.Models
{
    public class Test
    {
        [Required]
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public Course Course { get; set; }
        public List<Question> Questions { get; set; }
    }
}