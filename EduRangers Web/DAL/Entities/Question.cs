using BinderLayer.Interfaces;

namespace DAL.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public Test Test { get; set; }
    }
}