using System;
using System.Collections.Generic;
using System.Linq;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public static class UserRepositoryExtensions
    {
        public static User GetSingleByUsername(this IRepository<User, int> userRepository, string username)
        {
            var user = userRepository.GetAll().FirstOrDefault(u => u.UserName == username);
            return user;
        }
    }
}
