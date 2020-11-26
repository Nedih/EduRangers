using System.Collections.Generic;

namespace BinderLayer.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}