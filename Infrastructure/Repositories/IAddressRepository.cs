using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public interface IAddressRepository
{
    Task<List<AddressDto>> GetByUserIdAsync(int userId, CancellationToken ct);
    Task<bool> UserExistsAsync(int userId, CancellationToken ct);
    Task<AddressDto> CreateAsync(Address address, CancellationToken ct);
    Task<AddressDto?> UpdateAsync(int id, string? street, string? city, string? country, string? zipCode, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
