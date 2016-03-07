using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace JYSpaCinema.Infrastructure.EntityFramework.Uow
{
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
