using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        User GetSingleByUsername(string username);
    }
}
