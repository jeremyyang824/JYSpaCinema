using System.Data.Entity;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class RentalRepository : EFBaseRepository<Rental, int>, IRentalRepository
    {
        public RentalRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
