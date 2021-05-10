using Domain.One.Services;
using Microsoft.AspNetCore.Mvc;
using Useful.ActionResult;
using Useful.Controllers;

namespace Api.One.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class TaxaJurosController : ApiControllerBase
    {
        private readonly TaxaJurosService _taxaJurosService;

        public TaxaJurosController(TaxaJurosService taxaJurosService)
        {
            _taxaJurosService = taxaJurosService;
        }

        /// <summary>
        /// Obter a taxa de juros padrão de 1%
        /// </summary>
        /// <response code="200">Retorna a taxa de juros padrão</response>
        [HttpGet]
        public ActionResult<WebApiResultModel<double>> Get() => Ok(_taxaJurosService.GetTaxaJuros());
    }
}
