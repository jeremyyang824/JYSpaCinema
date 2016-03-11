using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JYSpaCinema.Service.AppServices;
using JYSpaCinema.Service.DTO;
using JYSpaCinema.Web.Infrastructure.Core;

namespace JYSpaCinema.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly AccountAppService _accountAppService;

        public AccountController(AccountAppService accountAppService)
        {
            this._accountAppService = accountAppService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginInputDto loginDto)
        {
            return CreateHttpResponse(request, () =>
            {
                bool loginResult = ModelState.IsValid && this._accountAppService.Login(loginDto);
                return request.CreateResponse(HttpStatusCode.OK, new { success = loginResult });
            });
        }
    }
}