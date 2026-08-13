using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<List<UserDto>> GetAllAsync(bool? isActive, CancellationToken ct);
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
    Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId, CancellationToken ct);
    Task<UserDto> CreateAsync(User user, CancellationToken ct);
    Task<UserDto?> UpdateAsync(User user, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
