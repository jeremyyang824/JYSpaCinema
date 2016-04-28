using System;
using System.Collections.Generic;
using System.Linq;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Domain.Repositories
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
    }
}
