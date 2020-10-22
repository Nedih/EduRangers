namespace BinderLayer.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public TestModel Test { get; set; }
    }
}