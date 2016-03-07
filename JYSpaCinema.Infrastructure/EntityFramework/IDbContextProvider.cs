using System;
using System.Data.Entity;

namespace JYSpaCinema.Infrastructure.EntityFramework
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
