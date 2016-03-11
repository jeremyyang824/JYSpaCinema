using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;

namespace JYSpaCinema.Infrastructure.EntityFramework.Repositories
{
    public static class StockRepositoryExtensions
    {
        public static IEnumerable<Stock> GetAvailableItems(this IRepository<Stock, int> stocksRepository, int movieId)
        {
            IEnumerable<Stock> availableItems 
                = stocksRepository.GetAll().Where(s => s.MovieId == movieId && s.IsAvailable);
            return availableItems;
        }
    }
}
