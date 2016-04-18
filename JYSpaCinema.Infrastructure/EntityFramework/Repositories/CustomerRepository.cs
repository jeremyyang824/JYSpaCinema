using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class CustomerRepository : EFBaseRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public bool UserExists(string email, string identityCard)
        {
            bool userExists = false;
            userExists = this.GetAll()
                .Any(c => c.Email.ToLower() == email || c.IdentityCard.ToLower() == identityCard);
            return userExists;
        }

        public string GetCustomerFullName(int customerId)
        {
            var customer = this.GetByKey(customerId);
            if (customer == null)
                throw new JYSpaCinemaDomainException("Customer:[{0}] not exists!", customerId);

            string customerName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
            return customerName;
        }
    }
}
