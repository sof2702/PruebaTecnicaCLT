using FluentValidation;
using MediatR;
using PruebaTecnicaCLT.Application.Addresses.Commands;
using PruebaTecnicaCLT.Application.Addresses.Queries;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Endpoints;

public static class DireccionesEndpoints
{
    public static void MapearEndpointsDirecciones(this WebApplication app)
    {
        app.MapGet("/users/{userId:int}/addresses", async (int userId, IMediator mediador) =>
        {
            try
            {
                var resultado = await mediador.Send(new GetAddressesByUserQuery(userId));
                return Results.Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        }).WithTags("Direcciones");

        app.MapPost("/users/{userId:int}/addresses", async (int userId, CreateAddressRequest solicitud, IValidator<CreateAddressRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            try
            {
                var resultado = await mediador.Send(new CreateAddressCommand(userId, solicitud.Street, solicitud.City, solicitud.Country, solicitud.ZipCode));
                return Results.Created($"/addresses/{resultado.Id}", resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        }).WithTags("Direcciones");

        app.MapPut("/addresses/{id:int}", async (int id, UpdateAddressRequest solicitud, IValidator<UpdateAddressRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            var resultado = await mediador.Send(new UpdateAddressCommand(id, solicitud.Street, solicitud.City, solicitud.Country, solicitud.ZipCode));
            return resultado is null ? Results.NotFound() : Results.Ok(resultado);
        }).WithTags("Direcciones");

        app.MapDelete("/addresses/{id:int}", async (int id, IMediator mediador) =>
        {
            var eliminado = await mediador.Send(new DeleteAddressCommand(id));
            return eliminado ? Results.NoContent() : Results.NotFound();
        }).WithTags("Direcciones");
    }
}
