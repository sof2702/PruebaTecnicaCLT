using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.CurrencyConversion;

public class ConvertCurrencyHandler : IRequestHandler<ConvertCurrencyCommand, ConvertCurrencyResponse>
{
    private readonly ICurrencyRepository _repository;

    public ConvertCurrencyHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<ConvertCurrencyResponse> Handle(ConvertCurrencyCommand request, CancellationToken ct)
    {
        var from = await _repository.GetByCodeAsync(request.FromCurrencyCode.ToUpperInvariant(), ct);
        if (from is null)
            throw new KeyNotFoundException($"Moneda '{request.FromCurrencyCode}' no encontrada.");

        var to = await _repository.GetByCodeAsync(request.ToCurrencyCode.ToUpperInvariant(), ct);
        if (to is null)
            throw new KeyNotFoundException($"Moneda '{request.ToCurrencyCode}' no encontrada.");

        var montoBase = request.Amount * from.RateToBase;
        var convertedAmount = montoBase / to.RateToBase;

        return new ConvertCurrencyResponse(from.Code, to.Code, request.Amount, convertedAmount);
    }
}
