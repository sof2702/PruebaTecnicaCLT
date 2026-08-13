using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(request.Id, ct);
        if (user is null) return null;

        if (!string.IsNullOrWhiteSpace(request.Name))
            user.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _repository.ExistsByEmailExcludingIdAsync(request.Email, request.Id, ct))
                throw new InvalidOperationException($"El email '{request.Email}' ya está en uso.");
            user.Email = request.Email;
        }

        if (request.IsActive.HasValue)
            user.IsActive = request.IsActive.Value;

        return await _repository.UpdateAsync(user, ct);
    }
}
