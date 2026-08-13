using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Currencies.Validators;

public class CrearMonedaValidador : AbstractValidator<CreateCurrencyRequest>
{
    public CrearMonedaValidador()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("El código es obligatorio.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.RateToBase).GreaterThan(0).WithMessage("La tasa debe ser mayor a 0.");
    }
}
