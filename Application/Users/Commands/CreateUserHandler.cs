using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsByEmailAsync(request.Email, ct))
            throw new InvalidOperationException($"El email '{request.Email}' ya está en uso.");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = string.Empty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(user, ct);
    }
}
