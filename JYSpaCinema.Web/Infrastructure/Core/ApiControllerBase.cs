using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JYSpaCinema.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                //模型验证
                if (!ModelState.IsValid)
                {
                    var errorMsg = new { success = false, errors = ModelState.Values.SelectMany(s => s.Errors).Select(e => e.ErrorMessage) };
                    return request.CreateResponse(HttpStatusCode.BadRequest, errorMsg);
                }

                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }
    }
}