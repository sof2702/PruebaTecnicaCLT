using MediatR;

namespace PruebaTecnicaCLT.Application.Addresses.Commands;

public record DeleteAddressCommand(int Id) : IRequest<bool>;
