using FluentValidation;
using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Application.Users.Commands;
using PruebaTecnicaCLT.Application.Users.Queries;

namespace PruebaTecnicaCLT.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapearEndpointsUsuarios(this WebApplication app)
    {
        var grupo = app.MapGroup("/users").WithTags("Usuarios");

        grupo.MapGet("/", async (bool? isActive, IMediator mediador) =>
        {
            var resultado = await mediador.Send(new GetUsersQuery(isActive));
            return Results.Ok(resultado);
        });

        grupo.MapGet("/{id:int}", async (int id, IMediator mediador) =>
        {
            var resultado = await mediador.Send(new GetUserByIdQuery(id));
            return resultado is null ? Results.NotFound() : Results.Ok(resultado);
        });

        grupo.MapPost("/", async (CreateUserRequest solicitud, IValidator<CreateUserRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            try
            {
                var resultado = await mediador.Send(new CreateUserCommand(solicitud.Name, solicitud.Email));
                return Results.Created($"/users/{resultado.Id}", resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        });

        grupo.MapPut("/{id:int}", async (int id, UpdateUserRequest solicitud, IValidator<UpdateUserRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            try
            {
                var resultado = await mediador.Send(new UpdateUserCommand(id, solicitud.Name, solicitud.Email, solicitud.IsActive));
                return resultado is null ? Results.NotFound() : Results.Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        });

        grupo.MapDelete("/{id:int}", async (int id, IMediator mediador) =>
        {
            var eliminado = await mediador.Send(new DeleteUserCommand(id));
            return eliminado ? Results.NoContent() : Results.NotFound();
        });
    }
}
