using MediatR;

namespace PruebaTecnicaCLT.Application.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<bool>;
