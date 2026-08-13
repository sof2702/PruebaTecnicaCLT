using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Addresses.Queries;

public record GetAddressesByUserQuery(int UserId) : IRequest<List<AddressDto>>;
