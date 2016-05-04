using System;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Infrastructure.EntityFramework.Configurations
{
    public class StockConfiguration : EntityBaseConfiguration<Stock, int>
    {
        public StockConfiguration()
        {
            Property(s => s.MovieId).IsRequired();
            Property(s => s.UniqueKey).IsRequired();
            Property(s => s.IsAvailable).IsRequired();
            HasMany(s => s.Rentals).WithRequired(r => r.Stock).HasForeignKey(r => r.StockId);
        }
    }
}
