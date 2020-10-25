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
    public class AttemptManager : IAttemptManager
    {
        private readonly IRepository repository;

        public AttemptManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateAttempt(AttemptModel model)
        {
            //Attempt attempt = new Attempt();

            var mapper = MapHelper.Mapping<AttemptModel, Attempt>();
            Attempt attempt = mapper.Map<Attempt>(model);

            this.repository.AddAndSave<Attempt>(attempt);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<AttemptModel> GetAttempt()
        {
            var mapper = MapHelper.Mapping<Attempt, AttemptModel>();
            return mapper.Map<List<AttemptModel>>(this.repository.GetAll<Attempt>());

        }

        public AttemptModel GetAttempt(Func<Attempt, bool> predicate)
        {

            var mapper = MapHelper.Mapping<AttemptModel, Attempt>();
            return mapper.Map<AttemptModel>(this.repository.FirstorDefault(predicate));
        }

        public AttemptModel GetAttemptById(int id)
        {

            var mapper = MapHelper.Mapping<AttemptModel, Attempt>();
            return mapper.Map<AttemptModel>(this.repository.FirstorDefault<Attempt>(x => x.Id == id));
        }

        public void RemoveAttempt(int id)
        {
            var attempt = this.repository.FirstorDefault<Attempt>(x => x.Id == id);
            if (attempt == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(attempt);
        }

        public void UpdateAttempt(int id, AttemptModel model)
        {
            var attempt = this.repository.FirstorDefault<Attempt>(x => x.Id == id);
            if (attempt == null)
                throw new NullReferenceException();
            var mapper = MapHelper.Mapping<Attempt, AttemptModel>();
            attempt = mapper.Map<Attempt>(model);

            this.repository.UpdateAndSave(attempt);
        }
    }
}
