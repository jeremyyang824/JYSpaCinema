using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JYSpaCinema.Domain;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Service.AppServices
{
    public class MovieAppService : BaseAppService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieAppService(
            IMovieRepository movieRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._movieRepository = movieRepository;
        }

        public MovieDto GetMovie(int id)
        {
            return this._movieRepository.GetByKey(id)
                .MapTo<MovieDto>();
        }

        public IEnumerable<MovieDto> GetLatestMovies()
        {
            return this._movieRepository.GetAll()
                .Include(m => m.Genre)
                .Include(m => m.Stocks)
                .OrderByDescending(m => m.ReleaseDate)
                .Take(6)
                .ToList()
                .MapTo<IReadOnlyCollection<MovieDto>>();
        }

        public PaginationResult<MovieDto> Search(int page = 1, int pageSize = 15, string filter = null)
        {
            Expression<Func<Movie, bool>> searchExpression = null;
            if (filter != null)
            {
                filter = filter.ToLower().Trim();
                searchExpression = (m => m.Title.ToLower().Contains(filter));
            }
            //IPagedList<Movie> result = this._movieRepository.FindByPager(searchExpression, page, pageSize,
            //    new SortExpression<Movie>[]
            //    {
            //        new SortExpression<Movie>(m => m.Title, SortOrder.Ascending)
            //    });

            //return result.MapToPagination<MovieDto>();

            var query = this._movieRepository.GetAll().WhereIf(searchExpression);
            var totalCount = query.Count();
            var items = query
                .Include(m => m.Genre)
                .Include(m => m.Stocks)
                .OrderBy(m => m.ID)
                .PageBy((page - 1) * pageSize, pageSize)
                .ToList()
                .MapTo<IReadOnlyList<MovieDto>>();
            return new PaginationResult<MovieDto>(totalCount, items);
        }

        public MovieDto AddMovie(MovieDto movie)
        {
            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                Movie newMovie = new Movie();
                this.updateMovie(newMovie, movie);

                //新建库存
                for (int i = 0; i < movie.NumberOfStocks; i++)
                {
                    Stock stock = new Stock()
                    {
                        IsAvailable = true,
                        Movie = newMovie,
                        UniqueKey = Guid.NewGuid()
                    };
                    newMovie.Stocks.Add(stock);
                }

                this._movieRepository.Add(newMovie);

                uow.Commit();

                return newMovie.MapTo<MovieDto>();
            }
        }

        public void UpdateMovie(MovieDto movie)
        {
            if (movie.ID <= 0)
                throw new ArgumentException("movie");

            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                Movie existsMovie = this._movieRepository.GetByKey(movie.ID);
                if (existsMovie == null)
                    throw new DomainException("Movie [{0}] not exists!", movie.ID);

                this.updateMovie(existsMovie, movie);
                this._movieRepository.Update(existsMovie);

                uow.Commit();
            }
        }

        public void UpdateMovieImage(int movieId, string imageName)
        {
            if (movieId <= 0)
                throw new ArgumentNullException("movieId");
            if (string.IsNullOrWhiteSpace(imageName))
                throw new ArgumentNullException("imageName");

            using (IUnitOfWork uow = this.UnitOfWorkManager.Begin(UnitOfWorkOptions.Default))
            {
                Movie existsMovie = this._movieRepository.GetByKey(movieId);
                if (existsMovie == null)
                    throw new DomainException("Movie [{0}] not exists!", movieId);

                existsMovie.Image = imageName;
                this._movieRepository.Update(existsMovie);

                uow.Commit();
            }
        }

        private void updateMovie(Movie movie, MovieDto source)
        {
            movie.Title = source.Title;
            movie.Description = source.Description;
            movie.GenreId = source.GenreId;
            movie.Director = source.Director;
            movie.Writer = source.Writer;
            movie.Producer = source.Producer;
            movie.Rating = source.Rating;
            movie.TrailerURI = source.TrailerURI;
            movie.ReleaseDate = source.ReleaseDate;
        }
    }
}
