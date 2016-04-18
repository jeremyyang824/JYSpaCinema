using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class UserRepository : EFBaseRepository<User, int>, IUserRepository
    {
        public UserRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public User GetSingleByUsername(string username)
        {
            var user = this.GetAll().FirstOrDefault(u => u.UserName == username);
            return user;
        }
    }
}
