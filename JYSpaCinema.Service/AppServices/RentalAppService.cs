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
using System.Data.Entity;
using JYSpaCinema.Domain;

namespace JYSpaCinema.Service.AppServices
{
    public class RentalAppService : BaseAppService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalAppService(
            IRentalRepository rentalRepository,
            IStockRepository stockRepository,
            IMovieRepository movieRepository,
            ICustomerRepository customerRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._rentalRepository = rentalRepository;
            this._stockRepository = stockRepository;
            this._movieRepository = movieRepository;
            this._customerRepository = customerRepository;
        }

        /// <summary>
        /// 统计所有影片的历史（每日）租借信息
        /// </summary>
        public List<TotalRentalHistoryDto> GetAllRentalHistory()
        {
            List<TotalRentalHistoryDto> totalHistoryList = new List<TotalRentalHistoryDto>();

            var movies = this._movieRepository.GetAll()
                .Include("Stocks.Rentals");
            foreach (var movie in movies)
            {
                TotalRentalHistoryDto totalRentalHistory = new TotalRentalHistoryDto
                {
                    ID = movie.ID,
                    Title = movie.Title,
                    Image = movie.Image,
                };

                //group by date
                IEnumerable<Rental> rentals = movie.Stocks.SelectMany(s => s.Rentals);
                IEnumerable<RentalHistoryPerDate> rentalPerDate = rentals.GroupBy(r => r.RetalDate.Date).Select(g => new RentalHistoryPerDate
                {
                    Date = g.Key,
                    TotalRentals = g.Count()
                });
                totalRentalHistory.Rentals = rentalPerDate.ToList();

                if (totalRentalHistory.TotalRentals > 0)
                    totalHistoryList.Add(totalRentalHistory);
            }
            return totalHistoryList;
        }

        /// <summary>
        /// 获取某影片的历史租借信息
        /// </summary>
        public List<RentalHistoryDto> GetRentalHistory(int movieId)
        {
            List<RentalHistoryDto> rentalHistory = new List<RentalHistoryDto>();

            Movie movie = this._movieRepository.GetAll()
                .Where(m => m.ID == movieId)
                .Include("Stocks.Rentals")
                .FirstOrDefault();

            if (movie == null)
                throw new DomainException("Movie:[{0}] not found!", movieId);

            foreach (Stock stock in movie.Stocks)
            {
                rentalHistory.AddRange(stock.Rentals.MapTo<IEnumerable<RentalHistoryDto>>());
            }

            rentalHistory.Sort((r1, r2) => r2.RetalDate.CompareTo(r1.RetalDate));
            return rentalHistory;
        }

        /// <summary>
        /// 租借一部影片
        /// </summary>
        public RentalDto Rent(int customerId, int stockId)
        {
            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                var customer = _customerRepository.GetByKey(customerId);
                if (customer == null)
                    throw new DomainException("Customer:[{0}] not found!", customerId);

                var stock = _stockRepository.GetByKey(stockId);
                if (stock == null)
                    throw new DomainException("Stock:[{0}] not found!", stockId);

                if (!stock.IsAvailable)
                    throw new DomainException("Stock:[{0}] not available!", stockId);

                Rental rental = new Rental
                {
                    CustomerId = customerId,
                    StockId = stockId,
                    RetalDate = DateTime.Now,
                    Status = "Borrowed"
                };
                this._rentalRepository.Add(rental);

                stock.IsAvailable = false;
                this._stockRepository.Update(stock);

                uow.Commit();

                return rental.MapTo<RentalDto>();
            }
        }

        /// <summary>
        /// 归还影片
        /// </summary>
        /// <param name="rentalId">租借ID</param>
        public void Return(int rentalId)
        {
            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                Rental rental = this._rentalRepository.GetByKey(rentalId);
                if (rental == null)
                    throw new DomainException("Rental:[{0}] not found!", rentalId);

                rental.Status = "Returned";
                rental.Stock.IsAvailable = true;
                rental.ReturnedDate = DateTime.Now;

                this._rentalRepository.Update(rental);

                uow.Commit();
            }
        }
    }
}
