using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
