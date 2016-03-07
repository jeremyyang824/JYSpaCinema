using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Domain.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        TEntity GetByKey(TKey id);

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] eager);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] eager);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
