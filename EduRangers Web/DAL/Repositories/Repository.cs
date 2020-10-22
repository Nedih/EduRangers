using BinderLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
