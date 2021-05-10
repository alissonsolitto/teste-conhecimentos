using Domain.Two.Models;
using FluentValidation;

namespace Domain.Two.Validators
{
    public class DadosCalcularJurosValidator : AbstractValidator<DadosCalcularJurosModel>
	{
		public DadosCalcularJurosValidator()
		{
			RuleFor(x => x.ValorInicial)
				.GreaterThan(0).WithMessage("Valor inicial deve ser maior que zero.");

			RuleFor(x => x.Meses)
				.GreaterThan(0).WithMessage("Quantidade de meses deve ser maior que zero.");
		}
	}
}
