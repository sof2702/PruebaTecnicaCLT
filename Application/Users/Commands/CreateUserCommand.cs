using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public record CreateUserCommand(string Name, string Email) : IRequest<UserDto>;
