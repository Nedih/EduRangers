using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IRepository repository;

        public QuestionManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateQuestion(QuestionModel model)
        {
            //Question question = new Question();

            var mapper = MapHelper.Mapping<QuestionModel, Question>(); 
            Question question = mapper.Map<Question>(model);
            question.Test = this.repository.FirstorDefault<Test>(x => x.Id == model.TestId);
            this.repository.AddAndSave<Question>(question);
        }
        public IEnumerable<QuestionModel> GetQuestions(int id)
        {
            var mapper = MapHelper.Mapping<Question, QuestionModelMap>();
            var mapper2 = MapHelper.Mapping<QuestionModelMap, QuestionModel>();
            var temp =  mapper2.Map<List<QuestionModel>>(mapper.Map<List<QuestionModelMap>>(this.repository.GetQuestionWhere<Question>(x => x.Test.Id == id)));
            foreach(var q in temp)
            {
                var ansv = this.repository.GetAnswerWhere<Answer>(x => x.Question.Id == q.Id);
                foreach (var t in ansv)
                {
                    q.AnswersString += t.AnswerText + ";   ";
                }
            }
            return temp;
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<QuestionModel> GetQuestion()
        {
            var mapper = MapHelper.Mapping<Question, QuestionModel>(); 
            return mapper.Map<List<QuestionModel>>(this.repository.GetAll<Question>());

        }

        public QuestionModel GetQuestion(Func<Question, bool> predicate)
        {

            var mapper = MapHelper.Mapping<Question, QuestionModel>();
            return mapper.Map<QuestionModel>(this.repository.FirstorDefault(predicate));
        }

        public QuestionModel GetQuestionById(int id)
        {

            var mapper = MapHelper.Mapping<Question, QuestionModel>();
            var temp =  mapper.Map<QuestionModel>(this.repository.FirstorDefault<Question>(x => x.Id == id));
            temp.AnswersString = this.repository.GetAnswerWhere<Answer>(x => x.Question.Id == id).ToString();
            return temp;
        }

        public void RemoveQuestion(int id)
        {
            var question = this.repository.FirstorDefault<Question>(x => x.Id == id);
            if (question == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(question);
        }

        public void UpdateQuestion(int id, QuestionModel model)
        {
            var question = this.repository.FirstorDefault<Question>(x => x.Id == id);
            if (question == null)
                throw new NullReferenceException();
            question.Id = id;
            if (model.QuestionText != null)
                question.QuestionText = model.QuestionText;

            this.repository.UpdateAndSave(question);
        }
    }
}
