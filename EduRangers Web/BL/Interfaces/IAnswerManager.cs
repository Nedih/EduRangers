using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAnswerManager
    {
        IEnumerable<AnswerModel> GetAnswer();

        AnswerModel GetAnswerById(int id);

        IEnumerable<AnswerModel> GetAnswers(int id);

        AnswerModel GetAnswer(Func<Answer, bool> predicate);

        void CreateAnswer(AnswerModel model);

        void UpdateAnswer(int id, AnswerModel model);

        void RemoveAnswer(int id);
    }
}

