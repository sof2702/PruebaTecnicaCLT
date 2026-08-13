using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Addresses.Queries;

public class GetAddressesByUserHandler : IRequestHandler<GetAddressesByUserQuery, List<AddressDto>>
{
    private readonly IAddressRepository _repository;

    public GetAddressesByUserHandler(IAddressRepository repository) => _repository = repository;

    public async Task<List<AddressDto>> Handle(GetAddressesByUserQuery request, CancellationToken ct)
    {
        if (!await _repository.UserExistsAsync(request.UserId, ct))
            throw new KeyNotFoundException($"Usuario con id {request.UserId} no encontrado.");

        return await _repository.GetByUserIdAsync(request.UserId, ct);
    }
}
