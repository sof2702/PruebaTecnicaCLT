using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Currencies.Commands;

public record CreateCurrencyCommand(string Code, string Name, decimal RateToBase) : IRequest<CurrencyDto>;
