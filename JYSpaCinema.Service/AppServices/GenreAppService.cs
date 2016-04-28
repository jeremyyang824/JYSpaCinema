using System.Collections.Generic;
using System.Linq;
using JYSpaCinema.Domain.Repositories;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Service.Uow;

namespace JYSpaCinema.Service.AppServices
{
    public class GenreAppService : BaseAppService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreAppService(
            IGenreRepository genreRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        {
            this._genreRepository = genreRepository;
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            return this._genreRepository.GetAll().ToList()
                .MapTo<IEnumerable<GenreDto>>();
        }
    }
}
