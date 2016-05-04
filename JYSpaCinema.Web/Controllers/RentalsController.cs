using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JYSpaCinema.Domain.ValueObjects;
using JYSpaCinema.Service.AppServices;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Web.Infrastructure.Core;

namespace JYSpaCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/rentals")]
    public class RentalsController : ApiControllerBase
    {
        private readonly RentalAppService _rentalAppService;

        public RentalsController(RentalAppService rentalAppService)
        {
            this._rentalAppService = rentalAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}/rentalhistory")]
        public HttpResponseMessage RentalHistory(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var historys = this._rentalAppService.GetRentalHistory(id);
                return request.CreateResponse<List<RentalHistoryDto>>(HttpStatusCode.OK, historys);
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("rentalhistory")]
        public HttpResponseMessage TotalRentalHistory(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var historys = this._rentalAppService.GetAllRentalHistory();
                return request.CreateResponse<List<TotalRentalHistoryDto>>(HttpStatusCode.OK, historys);
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("rent/{customerId:int}/{stockId:int}")]
        public HttpResponseMessage Rent(HttpRequestMessage request, int customerId, int stockId)
        {
            return CreateHttpResponse(request, () =>
            {
                var rentDto = this._rentalAppService.Rent(customerId, stockId);
                return request.CreateResponse<RentalDto>(HttpStatusCode.OK, rentDto);
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("return/{rentalId:int}")]
        public HttpResponseMessage Return(HttpRequestMessage request, int rentalId)
        {
            return CreateHttpResponse(request, () =>
            {
                this._rentalAppService.Return(rentalId);
                return request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}
