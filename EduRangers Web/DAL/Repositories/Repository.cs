using BinderLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository : IRepository
    {
        private DbContext context;

        public Repository(UserContext context)
        {
            this.context = context;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                    this.context = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        IList<TEntity> IRepository.GetTestWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Questions").Include("Course").Where(predicate).ToList();
        }

        IList<TEntity> IRepository.GetCourseAbilityWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Course").Include("Ability").Where(predicate).ToList();
        }

        IList<TEntity> IRepository.GetAttemptsWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Test").Include("Test.Course").Include("Test.Course.Author").Include("Student").Where(predicate).ToList();
        }

        IList<TEntity> IRepository.GetWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Where(predicate).ToList();
        }

        IList<TEntity> IRepository.GetAnswerWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Question").Where(predicate).ToList();
        }
        IList<TEntity> IRepository.GetCourseWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Author").Include("Tests").Where(predicate).ToList();
        }

        IList<TEntity> IRepository.GetQuestionWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Include("Test").Where(predicate).ToList();
        }

        double IRepository.AvgMark<TEntity>(Func<TEntity, bool> predicate)
        {
            return 0; //this.context.Set<TEntity>().Where(predicate)..Average();
        }

        void IRepository.AddAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }

        TEntity IRepository.FirstorDefault<TEntity>(Func<TEntity, bool> predicate) =>
           this.context.Set<TEntity>().FirstOrDefault(predicate);

        IList<TEntity> IRepository.GetAll<TEntity>()
        {
            return this.context.Set<TEntity>().ToList();
        }

        void IRepository.RemoveAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }

        void IRepository.UpdateAndSave<TEntity>(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
