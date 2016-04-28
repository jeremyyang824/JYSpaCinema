using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JYSpaCinema.Domain.Entities;
using JYSpaCinema.Domain.ValueObjects;
using JYSpaCinema.Service.AppServices;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Web.Infrastructure.Core;

namespace JYSpaCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/genres")]
    public class GenresController : ApiControllerBase
    {
        private readonly GenreAppService _genreAppService;

        public GenresController(GenreAppService genreAppService)
        {
            this._genreAppService = genreAppService;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var genres = this._genreAppService.GetAllGenres();
                return request.CreateResponse(HttpStatusCode.OK, genres);
            });
        }
    }
}