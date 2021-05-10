using Domain.Two.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Useful.ActionResult;
using Useful.Interfaces;
using Web.App.Models;

namespace Web.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRestService _restService;

        public HomeController(IConfiguration configuration, IRestService restService)
        {
            _configuration = configuration;
            _restService = restService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Github()
        {
            try
            {
                var result = await _restService.GetQueryAsync<string>(_configuration.GetValue<string>("ApiGateway:UrlApi"), "/api-two/showmethecode");

                if (result.Code != ReturnCode.Success)
                {
                    throw new Exception(result.Message);
                }

                return Redirect(result.Data);
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(DadosCalcularJurosModel dadosCalcularJuros)
        {
            try
            {
                if (!ModelState.IsValid) return View(dadosCalcularJuros);

                var result = await _restService.GetQueryAsync<double>(_configuration.GetValue<string>("ApiGateway:UrlApi"), "/api-two/calcularjuros", new Dictionary<string, string>
                {
                    { "ValorInicial", dadosCalcularJuros.ValorInicial.ToString() },
                    { "Meses", dadosCalcularJuros.Meses.ToString() }
                });

                if (result.Code != ReturnCode.Success)
                {
                    throw new Exception(result.Message);
                }

                ViewBag.ValorInvestimentoFinal = result.Data.ToString("C2");
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = ex.Message;
            }

            return View(dadosCalcularJuros);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
