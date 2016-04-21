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
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiControllerBase
    {
        private readonly CustomerAppService _customerAppService;

        public CustomersController(CustomerAppService customerAppService)
        {
            this._customerAppService = customerAppService;
        }

        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            return CreateHttpResponse(request, () =>
            {
                var customers = this._customerAppService.GetCustomers(filter);
                return request.CreateResponse(HttpStatusCode.OK, customers);
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var customer = this._customerAppService.GetCustomer(id);
                return request.CreateResponse(HttpStatusCode.OK, customer);
            });
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request, CustomerDto customer)
        {
            return CreateHttpResponse(request, () =>
            {
                var newCustomer = this._customerAppService.Register(customer);
                return request.CreateResponse<CustomerDto>(HttpStatusCode.Created, newCustomer);
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CustomerDto customer)
        {
            return CreateHttpResponse(request, () =>
            {
                this._customerAppService.Update(customer);
                return request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [HttpGet]
        [Route("search/{page:int=1}/{pageSize:int=15}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                var customers = this._customerAppService.Search(page, pageSize, filter);
                return request.CreateResponse(HttpStatusCode.OK, customers);
            });
        }
    }
}