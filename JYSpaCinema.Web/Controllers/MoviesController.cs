using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JYSpaCinema.Service.AppServices;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Web.Infrastructure.Core;

namespace JYSpaCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiControllerBase
    {
        private readonly MovieAppService _movieAppService;
        private readonly StockAppService _stockAppService;

        public MoviesController(MovieAppService movieAppService, StockAppService stockAppService)
        {
            this._movieAppService = movieAppService;
            this._stockAppService = stockAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var movies = this._movieAppService.GetLatestMovies();
                return request.CreateResponse(HttpStatusCode.OK, movies);
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var movie = this._movieAppService.GetMovie(id);
                return request.CreateResponse(HttpStatusCode.OK, movie);
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("search/{page:int=1}/{pageSize:int=15}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                var movies = this._movieAppService.Search(page, pageSize, filter);
                return request.CreateResponse(HttpStatusCode.OK, movies);
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, MovieDto movie)
        {
            return CreateHttpResponse(request, () =>
            {
                var newMovie = this._movieAppService.AddMovie(movie);
                return request.CreateResponse(HttpStatusCode.OK, newMovie);
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, MovieDto movie)
        {
            return CreateHttpResponse(request, () =>
            {
                this._movieAppService.UpdateMovie(movie);
                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("stocks/{id:int}")]
        public HttpResponseMessage GetStocks(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var stocks = this._stockAppService.GetMovieStock(id);
                return request.CreateResponse<IEnumerable<StockDto>>(HttpStatusCode.OK, stocks);
            });
        }
    }
}