using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Addresses.Commands;

public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, AddressDto>
{
    private readonly IAddressRepository _repository;

    public CreateAddressHandler(IAddressRepository repository) => _repository = repository;

    public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken ct)
    {
        if (!await _repository.UserExistsAsync(request.UserId, ct))
            throw new KeyNotFoundException($"Usuario con id {request.UserId} no encontrado.");

        var address = new Address
        {
            UserId = request.UserId,
            Street = request.Street,
            City = request.City,
            Country = request.Country,
            ZipCode = request.ZipCode
        };

        return await _repository.CreateAsync(address, ct);
    }
}
