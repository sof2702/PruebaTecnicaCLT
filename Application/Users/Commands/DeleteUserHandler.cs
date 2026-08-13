using MediatR;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken ct) =>
        await _repository.DeleteAsync(request.Id, ct);
}
