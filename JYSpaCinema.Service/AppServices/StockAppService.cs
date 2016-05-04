using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Domain.Services;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Service.AppServices
{
    public class StockAppService : BaseAppService
    {
        private readonly IStockRepository _stockRepository;

        public StockAppService(
            IStockRepository stockRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._stockRepository = stockRepository;
        }

        /// <summary>
        /// 获取影片可用库存
        /// </summary>
        public IEnumerable<StockDto> GetMovieStock(int movieId)
        {
            return this._stockRepository.GetAll()
                .Where(s => s.MovieId == movieId && s.IsAvailable)
                .MapTo<IEnumerable<StockDto>>();
        }
    }
}
