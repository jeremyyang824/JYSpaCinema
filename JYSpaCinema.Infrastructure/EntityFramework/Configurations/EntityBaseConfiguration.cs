using System;
using System.Data.Entity.ModelConfiguration;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Infrastructure.EntityFramework.Configurations
{
    public class EntityBaseConfiguration<TEntity, TKey> : EntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.ID);
        }
    }
}
