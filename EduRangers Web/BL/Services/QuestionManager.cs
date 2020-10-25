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

            this.repository.AddAndSave<Question>(question);
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

            var mapper = MapHelper.Mapping<QuestionModel, Question>();
            return mapper.Map<QuestionModel>(this.repository.FirstorDefault(predicate));
        }

        public QuestionModel GetQuestionById(int id)
        {

            var mapper = MapHelper.Mapping<QuestionModel, Question>();
            return mapper.Map<QuestionModel>(this.repository.FirstorDefault<Question>(x => x.Id == id));
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
            var mapper = MapHelper.Mapping<Question, QuestionModel>();
            question = mapper.Map<Question>(model);

            this.repository.UpdateAndSave(question);
        }
    }
}
