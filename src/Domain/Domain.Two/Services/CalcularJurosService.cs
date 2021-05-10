using System;
using System.Threading.Tasks;
using Useful.ActionResult;
using Useful.Interfaces;

namespace Domain.Two.Services
{
    public class CalcularJurosService
    {
        private readonly IRestService _restService;

        public CalcularJurosService(IRestService restService)
        {
            _restService = restService;
        }

        /// <summary>
        /// Calcular o valor final com base na taxa de juros obtida do serviço Api.One e quantidade de meses
        /// </summary>
        /// <param name="urlApiTaxaJuros"></param>
        /// <param name="valorInicial"></param>
        /// <param name="meses"></param>
        /// <returns></returns>
        public async Task<double> CalcularJurosTempo(string urlApiTaxaJuros, double valorInicial, int meses)
        {
            double taxaJuros = await GetTaxaJuros(urlApiTaxaJuros);
            return CalcularJuros(taxaJuros, valorInicial, meses);
        }

        /// <summary>
        /// Obter a taxa de juros do serviço Api.One
        /// </summary>
        /// <param name="urlApiTaxaJuros"></param>
        /// <returns></returns>
        private async Task<double> GetTaxaJuros(string urlApiTaxaJuros)
        {
            var response = await _restService.GetQueryAsync<double>(urlApiTaxaJuros, "api/v1/taxajuros");
            return response.Code != ReturnCode.Success ? throw new Exception(response.Message) : response.Data;
        }

        /// <summary>
        /// Calcular o valor final com base na taxa de juros e quantidade de meses
        /// </summary>
        /// <param name="taxaJuros"></param>
        /// <param name="valorInicial"></param>
        /// <param name="meses"></param>
        /// <returns></returns>
        private double CalcularJuros(double taxaJuros, double valorInicial, int meses)
        {
            double valorFinal = valorInicial * Math.Pow(1 + taxaJuros, meses);
            return Math.Truncate(valorFinal * 100) / 100;
        }
    }
}
