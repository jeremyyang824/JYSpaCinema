using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class RoleRepository : EFBaseRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
