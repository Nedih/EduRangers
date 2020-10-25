using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IQuestionManager
    {
        IEnumerable<QuestionModel> GetQuestion();

        QuestionModel GetQuestionById(int id);

        QuestionModel GetQuestion(Func<Question, bool> predicate);

        void CreateQuestion(QuestionModel model);

        void UpdateQuestion(int id, QuestionModel model);

        void RemoveQuestion(int id);
    }
}
