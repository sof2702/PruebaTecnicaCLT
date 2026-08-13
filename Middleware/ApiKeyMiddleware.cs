namespace PruebaTecnicaCLT.Middleware;

public class ApiKeyMiddleware
{
    private const string NombreHeaderApiKey = "X-API-KEY";
    private readonly RequestDelegate _siguiente;

    public ApiKeyMiddleware(RequestDelegate siguiente)
    {
        _siguiente = siguiente;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        var configuracion = contexto.RequestServices.GetRequiredService<IConfiguration>();
        var claveEsperada = configuracion["ApiKey"];

        if (!contexto.Request.Headers.TryGetValue(NombreHeaderApiKey, out var claveRecibida) ||
            claveRecibida != claveEsperada)
        {
            contexto.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await contexto.Response.WriteAsJsonAsync(new { error = "API Key inválida o ausente." });
            return;
        }

        await _siguiente(contexto);
    }
}
