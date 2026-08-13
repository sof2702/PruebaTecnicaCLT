using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public record UpdateUserCommand(int Id, string? Name, string? Email, bool? IsActive) : IRequest<UserDto?>;
