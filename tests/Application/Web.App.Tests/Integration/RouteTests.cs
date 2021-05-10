using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Web.App.Tests.Integration
{
    public class RouteTests : IClassFixture<WebApplicationFactory<Web.App.Startup>>
    {
        private readonly WebApplicationFactory<Web.App.Startup> _factory;

        public RouteTests(WebApplicationFactory<Web.App.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/", "Calcular Rendimentos - Web.App")]
        [InlineData("/Home", "Calcular Rendimentos - Web.App")]
        [InlineData("/Home/Github", "Github")]
        public async Task Validar_Rotas_Web_App(string url, string pageTitle)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            Assert.Contains(pageTitle, await response.Content.ReadAsStringAsync());
        }
    }
}
