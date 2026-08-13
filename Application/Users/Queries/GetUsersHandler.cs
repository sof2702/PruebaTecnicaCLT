using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Users.Queries;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IUserRepository repository) => _repository = repository;

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken ct) =>
        await _repository.GetAllAsync(request.IsActive, ct);
}
