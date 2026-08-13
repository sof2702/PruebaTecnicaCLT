using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Addresses.Commands;

public record UpdateAddressCommand(int Id, string? Street, string? City, string? Country, string? ZipCode)
    : IRequest<AddressDto?>;
