using MediatR;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Addresses.Commands;

public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly IAddressRepository _repository;

    public DeleteAddressHandler(IAddressRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken ct) =>
        await _repository.DeleteAsync(request.Id, ct);
}
