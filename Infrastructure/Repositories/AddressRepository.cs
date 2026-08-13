using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Data;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _db;

    public AddressRepository(AppDbContext db) => _db = db;

    public async Task<bool> UserExistsAsync(int userId, CancellationToken ct) =>
        await _db.Users.AnyAsync(u => u.Id == userId, ct);

    public async Task<List<AddressDto>> GetByUserIdAsync(int userId, CancellationToken ct) =>
        await _db.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => new AddressDto(a.Id, a.UserId, a.Street, a.City, a.Country, a.ZipCode))
            .ToListAsync(ct);

    public async Task<AddressDto> CreateAsync(Address address, CancellationToken ct)
    {
        _db.Addresses.Add(address);
        await _db.SaveChangesAsync(ct);
        return new AddressDto(address.Id, address.UserId, address.Street, address.City, address.Country, address.ZipCode);
    }

    public async Task<AddressDto?> UpdateAsync(int id, string? street, string? city, string? country, string? zipCode, CancellationToken ct)
    {
        var address = await _db.Addresses.FindAsync([id], ct);
        if (address is null) return null;

        if (!string.IsNullOrWhiteSpace(street)) address.Street = street;
        if (!string.IsNullOrWhiteSpace(city)) address.City = city;
        if (!string.IsNullOrWhiteSpace(country)) address.Country = country;
        if (zipCode is not null) address.ZipCode = zipCode;

        await _db.SaveChangesAsync(ct);
        return new AddressDto(address.Id, address.UserId, address.Street, address.City, address.Country, address.ZipCode);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var address = await _db.Addresses.FindAsync([id], ct);
        if (address is null) return false;

        _db.Addresses.Remove(address);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
