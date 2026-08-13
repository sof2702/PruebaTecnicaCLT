using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Addresses.Validators;

public class CrearDireccionValidador : AbstractValidator<CreateAddressRequest>
{
    public CrearDireccionValidador()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("La calle es obligatoria.");
        RuleFor(x => x.City).NotEmpty().WithMessage("La ciudad es obligatoria.");
        RuleFor(x => x.Country).NotEmpty().WithMessage("El país es obligatorio.");
    }
}
