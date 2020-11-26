using System.Collections.Generic;

namespace BinderLayer.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public string AnswersString { get; set; }
        public TestModel Test { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}