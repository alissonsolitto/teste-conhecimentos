using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Useful.ActionResult;
using Xunit;

namespace Api.One.Tests.Integration
{

    public class TaxaJurosTests : IClassFixture<WebApplicationFactory<Api.One.Startup>>
    {
        private readonly HttpClient httpClient;

        public TaxaJurosTests(WebApplicationFactory<Api.One.Startup> factory)
        {
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Validar_Taxa_Juros_Fixa()
        {
            var response = await httpClient.GetAsync("api/v1/taxajuros");
            response.EnsureSuccessStatusCode();
            
            var result = JsonConvert.DeserializeObject<WebApiResultModel<double>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(ReturnCode.Success, result.Code);
            Assert.Equal(0.01, result.Data);
            Assert.Equal(string.Empty, result.Message);
            Assert.Empty(result.Validators);
        }
    }
}