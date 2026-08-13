using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Addresses.Commands;

public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, AddressDto?>
{
    private readonly IAddressRepository _repository;

    public UpdateAddressHandler(IAddressRepository repository) => _repository = repository;

    public async Task<AddressDto?> Handle(UpdateAddressCommand request, CancellationToken ct) =>
        await _repository.UpdateAsync(request.Id, request.Street, request.City, request.Country, request.ZipCode, ct);
}
