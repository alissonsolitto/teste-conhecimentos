using Api.Two.V1.Querys;
using FluentValidation;

namespace Api.Two.V1.Validators
{
    public class CalcularJurosValidator : AbstractValidator<CalcularJurosQuery>
	{
		public CalcularJurosValidator()
		{
			RuleFor(x => x.ValorInicial)
				.GreaterThanOrEqualTo(0);

			RuleFor(x => x.Meses)
				.GreaterThan(0);
		}
	}
}
