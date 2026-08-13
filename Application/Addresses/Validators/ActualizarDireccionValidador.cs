using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Addresses.Validators;

public class ActualizarDireccionValidador : AbstractValidator<UpdateAddressRequest>
{
    public ActualizarDireccionValidador()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("La calle no puede estar vacía.")
            .When(x => x.Street is not null);
        RuleFor(x => x.City).NotEmpty().WithMessage("La ciudad no puede estar vacía.")
            .When(x => x.City is not null);
        RuleFor(x => x.Country).NotEmpty().WithMessage("El país no puede estar vacío.")
            .When(x => x.Country is not null);
    }
}
