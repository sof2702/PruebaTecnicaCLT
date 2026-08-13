using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Data;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task<List<UserDto>> GetAllAsync(bool? isActive, CancellationToken ct)
    {
        var query = _db.Users.AsQueryable();

        if (isActive.HasValue)
            query = query.Where(u => u.IsActive == isActive.Value);

        return await query
            .Select(u => new UserDto(u.Id, u.Name, u.Email, u.IsActive, u.CreatedAt))
            .ToListAsync(ct);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct) =>
        await _db.Users.FindAsync([id], ct);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct) =>
        await _db.Users.AnyAsync(u => u.Email == email, ct);

    public async Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId, CancellationToken ct) =>
        await _db.Users.AnyAsync(u => u.Email == email && u.Id != excludeId, ct);

    public async Task<UserDto> CreateAsync(User user, CancellationToken ct)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return new UserDto(user.Id, user.Name, user.Email, user.IsActive, user.CreatedAt);
    }

    public async Task<UserDto?> UpdateAsync(User user, CancellationToken ct)
    {
        await _db.SaveChangesAsync(ct);
        return new UserDto(user.Id, user.Name, user.Email, user.IsActive, user.CreatedAt);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var user = await _db.Users.FindAsync([id], ct);
        if (user is null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
