using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Users.Queries;

public record GetUsersQuery(bool? IsActive) : IRequest<List<UserDto>>;
