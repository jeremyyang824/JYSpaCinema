using System;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Infrastructure.EntityFramework.Configurations
{
    public class RentalConfiguration : EntityBaseConfiguration<Rental, int>
    {
        public RentalConfiguration()
        {
            Property(r => r.CustomerId).IsRequired();
            Property(r => r.StockId).IsRequired();
            Property(r => r.Status).IsRequired().HasMaxLength(10);
            Property(r => r.ReturnedDate).IsOptional();

            HasRequired(r => r.Customer).WithMany().HasForeignKey(t => t.CustomerId);
            HasRequired(r => r.Stock).WithMany().HasForeignKey(t => t.StockId);
        }
    }
}
