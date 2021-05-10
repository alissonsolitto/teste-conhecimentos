using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Useful.ActionResult;
using Xunit;

namespace Api.One.Tests.Integration
{
    public class CalcularJurosTests : IClassFixture<WebApplicationFactory<Api.Two.Startup>>
    {
        private readonly HttpClient httpClientTwo;

        public CalcularJurosTests(WebApplicationFactory<Api.Two.Startup> factoryTwo)
        {
            httpClientTwo = factoryTwo.CreateClient();
        }

        [Theory (Skip = "Git Actions - Precisa da Api.One em execução.")]
        [InlineData(1200, 12, 1352.19)]
        [InlineData(2000, 12, 2253.65)]
        [InlineData(1000, 5, 1051.01)]
        public async Task Validar_Calculo_Juros(double valorInicial, int meses, double resultado)
        {
            var resource = $"api/v1/calcularjuros?ValorInicial={valorInicial}&Meses={meses}";
            var response = await httpClientTwo.GetAsync(resource);

            var result = JsonConvert.DeserializeObject<WebApiResultModel<double>>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Assert.Equal(ReturnCode.Success, result.Code);
            Assert.Equal(resultado, result.Data);
            Assert.Equal(string.Empty, result.Message);
            Assert.Empty(result.Validators);
        }

        [Theory(Skip = "Git Actions - Precisa da Api.One em execução.")]
        [InlineData(0, 0, 0)]
        [InlineData(-1, 0, 0)]
        [InlineData(5, 0, 0)]
        public async Task Validar_Calculo_Juros_Parametros_Invalidos(double valorInicial, int meses, double resultado)
        {
            var resource = $"api/v1/calcularjuros?ValorInicial={valorInicial}&Meses={meses}";
            var response = await httpClientTwo.GetAsync(resource);

            var result = JsonConvert.DeserializeObject<WebApiResultModel<double>>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Assert.Equal(ReturnCode.Warning, result.Code);
            Assert.Equal(resultado, result.Data);
            Assert.Equal("Dados incorretos.", result.Message);
            Assert.NotEmpty(result.Validators);
        }

        [Fact]
        public async Task Validar_Url_Github()
        {
            var resource = $"api/v1/showmethecode";
            var response = await httpClientTwo.GetAsync(resource);

            var result = JsonConvert.DeserializeObject<WebApiResultModel<string>>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Assert.Equal(ReturnCode.Success, result.Code);
            Assert.Equal("http://github.com/alissonsolitto/teste-conhecimentos", result.Data);
            Assert.Equal(string.Empty, result.Message);
            Assert.Empty(result.Validators);
        }
    }
}