using BinderLayer.DTO;
using System.Collections.Generic;

namespace BinderLayer.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public ProfessorDTO Author { get; set; }
        public string AuthorEmail { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public double AvgMark { get; set; }
        public List<TestModel> Tests { get; set; }
        public List<SelectModel> Abilities { get; set; }
    }
}