namespace PruebaTecnicaCLT.Application.Common.Dtos;

// --- Users ---
public record UserDto(int Id, string Name, string Email, bool IsActive, DateTime CreatedAt);

public record CreateUserRequest(string Name, string Email);

public record UpdateUserRequest(string? Name, string? Email, bool? IsActive);

// --- Addresses ---
public record AddressDto(int Id, int UserId, string Street, string City, string Country, string? ZipCode);

public record CreateAddressRequest(string Street, string City, string Country, string? ZipCode);

public record UpdateAddressRequest(string? Street, string? City, string? Country, string? ZipCode);

// --- Currencies ---
public record CurrencyDto(int Id, string Code, string Name, decimal RateToBase);

public record CreateCurrencyRequest(string Code, string Name, decimal RateToBase);

// --- Currency Conversion ---
public record ConvertCurrencyRequest(string FromCurrencyCode, string ToCurrencyCode, decimal Amount);

public record ConvertCurrencyResponse(
    string FromCurrency,
    string ToCurrency,
    decimal OriginalAmount,
    decimal ConvertedAmount);
