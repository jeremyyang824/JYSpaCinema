using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.ValueObjects;

namespace JYSpaCinema.Domain.Services
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
    }
}
