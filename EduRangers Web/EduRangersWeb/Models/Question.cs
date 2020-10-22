using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduRangersWeb.Models
{
    public class Question
    {
        [Required]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public Test Test { get; set; }
    }
}