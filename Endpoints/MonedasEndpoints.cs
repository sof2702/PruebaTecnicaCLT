using FluentValidation;
using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Application.Currencies.Commands;
using PruebaTecnicaCLT.Application.Currencies.Queries;
using PruebaTecnicaCLT.Application.CurrencyConversion;

namespace PruebaTecnicaCLT.Endpoints;

public static class MonedasEndpoints
{
    public static void MapearEndpointsMonedas(this WebApplication app)
    {
        var grupo = app.MapGroup("/currencies").WithTags("Monedas");

        grupo.MapGet("/", async (IMediator mediador) =>
        {
            var resultado = await mediador.Send(new GetCurrenciesQuery());
            return Results.Ok(resultado);
        });

        grupo.MapPost("/", async (CreateCurrencyRequest solicitud, IValidator<CreateCurrencyRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            try
            {
                var resultado = await mediador.Send(new CreateCurrencyCommand(solicitud.Code, solicitud.Name, solicitud.RateToBase));
                return Results.Created($"/currencies/{resultado.Id}", resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        });

        app.MapPost("/currency/convert", async (ConvertCurrencyRequest solicitud, IValidator<ConvertCurrencyRequest> validador, IMediator mediador) =>
        {
            var validacion = await validador.ValidateAsync(solicitud);
            if (!validacion.IsValid)
                return Results.ValidationProblem(validacion.ToDictionary());

            try
            {
                var resultado = await mediador.Send(new ConvertCurrencyCommand(solicitud.FromCurrencyCode, solicitud.ToCurrencyCode, solicitud.Amount));
                return Results.Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        }).WithTags("Conversión");
    }
}
