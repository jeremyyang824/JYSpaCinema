using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public static class CustomerRepositoryExtensions
    {
        public static bool UserExists(this IRepository<Customer, int> customersRepository, string email, string identityCard)
        {
            bool userExists = false;
            userExists = customersRepository.GetAll()
                .Any(c => c.Email.ToLower() == email || c.IdentityCard.ToLower() == identityCard);
            return userExists;
        }

        public static string GetCustomerFullName(this IRepository<Customer, int> customersRepository, int customerId)
        {
            var customer = customersRepository.GetByKey(customerId);
            if (customer == null)
                throw new JYSpaCinemaDomainException("Customer:[{0}] not exists!", customerId);

            string customerName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
            return customerName;
        }
    }
}
