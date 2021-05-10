using Domain.One.Services;
using Xunit;

namespace Domain.One.Tests.Unit
{
    public class TaxaJurosServiceTests
    {
        [Fact]
        public void Validar_Taxa_Juros_Fixa()
        {
            Assert.Equal(0.01, new TaxaJurosService().GetTaxaJuros());
        }
    }
}
