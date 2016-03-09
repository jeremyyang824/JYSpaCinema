using System;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Infrastructure.EntityFramework.Configurations
{
    public class CustomerConfiguration : EntityBaseConfiguration<Customer, int>
    {
        public CustomerConfiguration()
        {
            Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            Property(u => u.LastName).IsRequired().HasMaxLength(100);
            Property(u => u.IdentityCard).IsRequired().HasMaxLength(50);
            Property(c => c.Mobile).HasMaxLength(10);
            Property(c => c.Email).IsRequired().HasMaxLength(200);
            Property(c => c.DateOfBirth).IsRequired();
        }
    }
}
