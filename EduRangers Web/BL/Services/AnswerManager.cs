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
    public class AnswerManager : IAnswerManager
    {
        private readonly IRepository repository;

        public AnswerManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateAnswer(AnswerModel model)
        {
            //Answer answer = new Answer();

            var mapper = MapHelper.Mapping<AnswerModel, Answer>();
            Answer answer = mapper.Map<Answer>(model);
            answer.Question = this.repository.FirstorDefault<Question>(x => x.Id == model.QuestionId);
            this.repository.AddAndSave<Answer>(answer);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<AnswerModel> GetAnswer()
        {
            var mapper = MapHelper.Mapping<Answer, AnswerModel>();
            return mapper.Map<List<AnswerModel>>(this.repository.GetAll<Answer>());

        }

        public IEnumerable<AnswerModel> GetAnswers(int id)
        {
             var mapper = MapHelper.Mapping<Answer, AnswerModelMap>();
             var mapper2 = MapHelper.Mapping<AnswerModelMap, AnswerModel>();
             var temp = mapper2.Map<List<AnswerModel>>(mapper.Map<List<AnswerModelMap>>(this.repository.GetAnswerWhere<Answer>(x => x.Question.Id == id)));
             return temp;
        }

        public AnswerModel GetAnswer(Func<Answer, bool> predicate)
        {

            var mapper = MapHelper.Mapping<Answer, AnswerModel>();
            return mapper.Map<AnswerModel>(this.repository.FirstorDefault(predicate));
        }

        public AnswerModel GetAnswerById(int id)
        {

            var mapper = MapHelper.Mapping<Answer, AnswerModel>();
            return mapper.Map<AnswerModel>(this.repository.FirstorDefault<Answer>(x => x.Id == id));
        }

        public void RemoveAnswer(int id)
        {
            var answer = this.repository.FirstorDefault<Answer>(x => x.Id == id);
            if (answer == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(answer);
        }

        public void UpdateAnswer(int id, AnswerModel model)
        {
            var answer = this.repository.FirstorDefault<Answer>(x => x.Id == id);
            if (answer == null)
                throw new NullReferenceException();
            var mapper = MapHelper.Mapping<AnswerModel, Answer>();
            //model.Id = id;
            answer = mapper.Map<Answer>(model);
            //answer.Id = null;
            answer.Question = this.repository.FirstorDefault<Question>(x => x.Id == model.QuestionId);
            this.repository.UpdateAndSave(answer);
        }

    }
}
