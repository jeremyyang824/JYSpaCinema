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

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="request">认证请求</param>
        /// <param name="loginDto">认证输入DTO</param>
        /// <returns>认证响应</returns>
        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginInputDto loginDto)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

                bool loginResult = this._accountAppService.Login(loginDto);
                return request.CreateResponse(HttpStatusCode.OK, new { success = loginResult });
            });
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="request">注册请求</param>
        /// <param name="registrationDto">注册输入DTO</param>
        /// <returns>注册响应</returns>
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationInputDto registrationDto)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!ModelState.IsValid)
                    return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

                bool registerResult = this._accountAppService.Register(registrationDto);
                return request.CreateResponse(HttpStatusCode.OK, new { success = registerResult });
            });
        }
    }
}