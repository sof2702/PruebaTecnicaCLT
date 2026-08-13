using FluentValidation;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Validators;

public class CrearUsuarioValidador : AbstractValidator<CreateUserRequest>
{
    public CrearUsuarioValidador()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email debe tener un formato válido.");
    }
}
