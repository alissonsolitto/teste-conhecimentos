using Microsoft.AspNetCore.Mvc;
using System;
using Useful.ActionResult;

namespace Useful.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        public virtual OkObjectResult Ok<T>(T data)
        {
            var customResult = new WebApiResultModel<T>(data);
            return new OkObjectResult(customResult);
        }

        public virtual OkObjectResult Ok<T>(T data, string message = "")
        {
            var customResult = new WebApiResultModel<T>(data, message: message);
            return new OkObjectResult(customResult);
        }

        [NonAction]
        public virtual BadRequestObjectResult BadRequest(string message = "", Exception exception = null)
        {
            var customResult = new WebApiResultModel<object>(message, exception);
            return new BadRequestObjectResult(customResult);
        }
    }
}
