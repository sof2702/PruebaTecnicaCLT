using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.CurrencyConversion.Validators;

public class ConvertirMonedaValidador : AbstractValidator<ConvertCurrencyRequest>
{
    public ConvertirMonedaValidador()
    {
        RuleFor(x => x.FromCurrencyCode).NotEmpty().WithMessage("El código de moneda origen es obligatorio.");
        RuleFor(x => x.ToCurrencyCode).NotEmpty().WithMessage("El código de moneda destino es obligatorio.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
    }
}
