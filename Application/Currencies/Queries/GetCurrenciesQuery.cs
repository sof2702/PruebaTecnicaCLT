using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.Currencies.Queries;

public record GetCurrenciesQuery : IRequest<List<CurrencyDto>>;
