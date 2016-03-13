using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class EFBaseRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;  //DbContext工厂

        protected virtual DbContext Context
        {
            get { return this._dbContextProvider.DbContext; }
        }

        public EFBaseRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            this._dbContextProvider = dbContextProvider;
        }

        #region methods

        public void Add(TEntity entity)
        {
            //DbEntityEntry dbEntityEntry = Context.Entry(entity);
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            DbEntityEntry dbEntiyEntry = Context.Entry(entity);
            dbEntiyEntry.State = EntityState.Deleted;
        }


        public TEntity GetByKey(TKey id)
        {
            return Context.Set<TEntity>().FirstOrDefault(e => e.ID.Equals(id));
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] eager)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var includeProp in eager)
            {
                query = query.Include(includeProp);
            }
            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] eager)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            query = query.Where(predicate);
            foreach (var includeProp in eager)
            {
                query = query.Include(includeProp);
            }
            return query;
        }

        #endregion
    }
}
