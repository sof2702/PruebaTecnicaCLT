using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;

namespace PruebaTecnicaCLT.Application.CurrencyConversion;

public record ConvertCurrencyCommand(string FromCurrencyCode, string ToCurrencyCode, decimal Amount)
    : IRequest<ConvertCurrencyResponse>;
