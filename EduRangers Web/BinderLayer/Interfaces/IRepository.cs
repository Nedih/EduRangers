using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderLayer.Interfaces
{
    public interface IRepository : IDisposable
    {
        IList<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;
        IList<TEntity> GetCourseWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        IList<TEntity> GetTestWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        IList<TEntity> GetQuestionWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        IList<TEntity> GetAnswerWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        IList<TEntity> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;

        TEntity FirstorDefault<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;

        void AddAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void UpdateAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void RemoveAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task SaveAsync();
    }
}
