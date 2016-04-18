using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public class StockRepository : EFBaseRepository<Stock, int>, IStockRepository
    {
        public StockRepository(IDbContextProvider<DbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public IEnumerable<Stock> GetAvailableItems(int movieId)
        {
            IEnumerable<Stock> availableItems
                = this.GetAll().Where(s => s.MovieId == movieId && s.IsAvailable);
            return availableItems;
        }
    }
}
