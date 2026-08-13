# Prueba Técnica CLT - API REST .NET 10

## Versión de .NET

Este proyecto fue desarrollado con .NET 10 usando Minimal API.

## Cómo correr el proyecto

Clonar el repositorio y ejecutar los siguientes comandos desde la carpeta del proyecto:

```bash
cd PruebaTecnicaCLT
dotnet run
```

Una vez levantado, el Swagger UI estará disponible directamente en la raíz de la aplicación.
El puerto exacto se muestra en la consola al iniciar (por ejemplo `https://localhost:7150`).

## Base de datos

Se usa SQLite. La base de datos se crea automáticamente al iniciar la aplicación gracias a `EnsureCreated()`, sin necesidad de ejecutar migraciones manualmente. El archivo generado se llama `prueba_tecnica.db` y queda en el directorio del proyecto.

Si se prefiere usar migraciones de EF Core en lugar de `EnsureCreated`, los comandos son:

```bash
dotnet ef migrations add MigracionInicial
dotnet ef database update
```

## Seguridad - API Key

Todos los endpoints requieren el siguiente header en cada request:

```
X-API-KEY: clave-secreta-prueba-2024
```

La clave está configurada en `appsettings.json` bajo la propiedad `ApiKey`. Si el header no se envía o el valor es incorrecto, la API responde con 401 Unauthorized.

### Cómo autorizar en Swagger UI

1. Abrir el Swagger en el navegador.
2. Hacer clic en el botón "Authorize" en la parte superior derecha.
3. Ingresar el valor: `clave-secreta-prueba-2024`
4. Hacer clic en "Authorize" y luego en "Close".

A partir de ese momento todas las peticiones desde Swagger incluirán el header automáticamente.

### Ejemplo con curl

```bash
curl -H "X-API-KEY: clave-secreta-prueba-2024" https://localhost:7150/users
```

## Endpoints disponibles

### Usuarios

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /users | Listar todos los usuarios. Acepta filtro opcional `?isActive=true` o `?isActive=false` |
| GET | /users/{id} | Obtener un usuario por su id |
| POST | /users | Crear un usuario nuevo |
| PUT | /users/{id} | Actualizar nombre, email o estado activo de un usuario |
| DELETE | /users/{id} | Eliminar un usuario |

Body para crear usuario:
```json
{
  "name": "Juan",
  "email": "juan@test.com"
}
```

### Direcciones

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /users/{userId}/addresses | Listar las direcciones de un usuario |
| POST | /users/{userId}/addresses | Crear una dirección para un usuario |
| PUT | /addresses/{id} | Actualizar una dirección |
| DELETE | /addresses/{id} | Eliminar una dirección |

Body para crear dirección:
```json
{
  "street": "Calle Falsa 123",
  "city": "Asunción",
  "country": "Paraguay",
  "zipCode": "9999"
}
```

### Monedas

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /currencies | Listar todas las monedas |
| POST | /currencies | Crear una moneda nueva |
| POST | /currency/convert | Convertir un monto entre dos monedas |

Body para conversión:
```json
{
  "fromCurrencyCode": "USD",
  "toCurrencyCode": "PYG",
  "amount": 100
}
```

## Qué quedó implementado

Se completaron todos los puntos solicitados en la prueba:

- CRUD completo de Users con validaciones por FluentValidation
- CRUD completo de Addresses relacionado a Users (relación 1:N), con validación de userId existente
- Módulo de Currencies: listar, crear y convertir entre monedas usando RateToBase
- Seguridad por API Key configurada en appsettings.json con middleware que retorna 401 si falta o es incorrecta
- Entity Framework Core con SQLite y DbContext con DbSet para User, Address y Currency
- FluentValidation en todos los requests: crear/editar usuario, crear/editar dirección, crear moneda y conversión de divisas
- Patrón CQRS implementado con MediatR, con carpetas separadas de Commands y Queries por módulo
- Swagger UI configurado con soporte de API Key mediante el botón Authorize
- Proyecto desarrollado con .NET 10 usando Minimal API

## Qué no quedó implementado

- Tests unitarios o de integración
- Paginación en los listados
