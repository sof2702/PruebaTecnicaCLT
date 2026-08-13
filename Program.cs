using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCLT.Endpoints;
using PruebaTecnicaCLT.Infrastructure.Data;
using PruebaTecnicaCLT.Infrastructure.Repositories;
using PruebaTecnicaCLT.Middleware;

var constructor = WebApplication.CreateBuilder(args);

constructor.Services.AddDbContext<AppDbContext>(opciones =>
    opciones.UseSqlite(constructor.Configuration.GetConnectionString("BaseDeDatos")));

constructor.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

constructor.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

constructor.Services.AddScoped<IUserRepository, UserRepository>();
constructor.Services.AddScoped<IAddressRepository, AddressRepository>();
constructor.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

constructor.Services.AddEndpointsApiExplorer();
constructor.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new() { Title = "Prueba Técnica CLT", Version = "v1" });
    opciones.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Ingrese la API Key en el header X-API-KEY"
    });
    opciones.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = constructor.Build();

using (var alcance = app.Services.CreateScope())
{
    var bd = alcance.ServiceProvider.GetRequiredService<AppDbContext>();
    bd.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(opciones =>
{
    opciones.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Técnica CLT v1");
    opciones.RoutePrefix = string.Empty;
});

app.UseMiddleware<ApiKeyMiddleware>();

app.MapearEndpointsUsuarios();
app.MapearEndpointsDirecciones();
app.MapearEndpointsMonedas();

app.Run();
