using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Users.Queries;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(request.Id, ct);
        if (user is null) return null;
        return new UserDto(user.Id, user.Name, user.Email, user.IsActive, user.CreatedAt);
    }
}
