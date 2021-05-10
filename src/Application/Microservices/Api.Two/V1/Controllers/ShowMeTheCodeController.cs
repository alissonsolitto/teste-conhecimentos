using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Useful.ActionResult;
using Useful.Controllers;

namespace Api.Two.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ShowMeTheCodeController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;

        public ShowMeTheCodeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Retornar a url do diretório do projeto no Github
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<WebApiResultModel<string>> Get()
        {
            var urlRepo = _configuration.GetValue<string>("GitHub:UrlRepo");
            return Ok(urlRepo);
        }

    }
}
