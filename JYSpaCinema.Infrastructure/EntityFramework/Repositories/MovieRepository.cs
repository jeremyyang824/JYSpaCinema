using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class MovieRepository : EFBaseRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
