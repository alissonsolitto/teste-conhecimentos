using Api.Two.V1.Querys;
using Domain.Two.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Useful.ActionResult;
using Useful.Controllers;

namespace Api.Two.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class CalcularJurosController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CalcularJurosService _calcularJurosService;

        public CalcularJurosController(IConfiguration configuration,
                                       CalcularJurosService calcularJurosService)
        {
            _configuration = configuration;
            _calcularJurosService = calcularJurosService;
        }

        /// <summary>
        /// Retornar o valor final após o calculo de juros em relação aos meses
        /// </summary>
        /// <param name="calculaJurosQuery"></param>
        /// <returns></returns>
        [HttpGet]        
        public async Task<ActionResult<WebApiResultModel<double>>> Get([FromQuery] CalcularJurosQuery calculaJurosQuery)
        {
            var urlApi = _configuration.GetValue<string>("ApiOne:UrlApi");
            return Ok(await _calcularJurosService.CalcularJurosTempo(urlApi, calculaJurosQuery.ValorInicial, calculaJurosQuery.Meses));
        }
    }
}
