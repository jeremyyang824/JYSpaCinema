using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        bool UserExists(string email, string identityCard);

        string GetCustomerFullName(int customerId);
    }
}
