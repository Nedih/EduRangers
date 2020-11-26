using BinderLayer.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public Test Test { get; set; }
    }
}