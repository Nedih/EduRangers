using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IQuestionionManager
    {
        IEnumerable<Question> GetQuestion();

        Question GetQuestionById(int id);

        Question GetQuestion(Func<Question, bool> predicate);

        void CreateQuestion(QuestionModel model);

        void UpdateQuestion(int id, QuestionModel model);

        void RemoveQuestion(int id);
    }
}
