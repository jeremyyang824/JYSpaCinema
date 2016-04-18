using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using JYSpaCinema.Domain;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class EFBaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        private readonly IDbContextProvider<DbContext> _dbContextProvider;  //DbContext工厂

        protected virtual DbContext Context
        {
            get { return this._dbContextProvider.DbContext; }
        }

        public EFBaseRepository(IDbContextProvider<DbContext> dbContextProvider)
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
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] eager)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return eager == null ? query : eager.Aggregate(query, (q, includeProp) => q.Include(includeProp));
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] eager)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (predicate != null)
                query = query.Where(predicate);
            return eager == null ? query : eager.Aggregate(query, (q, includeProp) => q.Include(includeProp));
        }

        public IPagedList<TEntity> FindByPager(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int pageSize,
            IEnumerable<Tuple<Expression<Func<TEntity, object>>, SortOrder>> sortExpression,
            params Expression<Func<TEntity, object>>[] eager)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (predicate != null)
                query = query.Where(predicate);

            int totalCount = query.Count();

            query = this.SortQuery(query, sortExpression)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (eager != null)
                query = eager.Aggregate(query, (q, includeProp) => q.Include(includeProp));

            return new PagedList<TEntity>(query.ToList(), pageNumber, pageSize, totalCount);
        }

        protected virtual IQueryable<TEntity> SortQuery(IQueryable<TEntity> queryable, IEnumerable<Tuple<Expression<Func<TEntity, object>>, SortOrder>> sortExpression)
        {
            int idx = 0;
            if (sortExpression != null)
            {
                foreach (var sortExp in sortExpression)
                {
                    switch (sortExp.Item2)
                    {
                        case SortOrder.Ascending:
                            if (sortExp.Item1 != null)
                            {
                                queryable = idx == 0
                                    ? queryable.OrderBy(sortExp.Item1)
                                    : ((IOrderedQueryable<TEntity>)queryable).ThenBy(sortExp.Item1);
                                idx++;
                            }
                            break;
                        case SortOrder.Descending:
                            if (sortExp.Item1 != null)
                            {
                                queryable = idx == 0
                                    ? queryable.OrderByDescending(sortExp.Item1)
                                    : ((IOrderedQueryable<TEntity>)queryable).ThenByDescending(sortExp.Item1);
                                idx++;
                            }
                            break;
                    }
                }
            }
            return queryable;
        }

        #endregion
    }
}
