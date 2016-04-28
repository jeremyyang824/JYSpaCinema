using System.Data.Entity;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class GenreRepository : EFBaseRepository<Genre, int>, IGenreRepository
    {
        public GenreRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
