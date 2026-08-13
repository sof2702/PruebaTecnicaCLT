using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Validators;

public class ActualizarUsuarioValidador : AbstractValidator<UpdateUserRequest>
{
    public ActualizarUsuarioValidador()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El email debe tener un formato válido.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
