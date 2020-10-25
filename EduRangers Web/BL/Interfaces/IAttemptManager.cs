using DAL.Entities;
using BinderLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAttemptManager
    {
        IEnumerable<AttemptModel> GetAttempt();

        AttemptModel GetAttemptById(int id);

        AttemptModel GetAttempt(Func<Attempt, bool> predicate);

        void CreateAttempt(AttemptModel model);

        void UpdateAttempt(int id, AttemptModel model);

        void RemoveAttempt(int id);
    }
}
