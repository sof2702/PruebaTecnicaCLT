using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Currencies.Commands;

public class CreateCurrencyHandler : IRequestHandler<CreateCurrencyCommand, CurrencyDto>
{
    private readonly ICurrencyRepository _repository;

    public CreateCurrencyHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<CurrencyDto> Handle(CreateCurrencyCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsByCodeAsync(request.Code.ToUpperInvariant(), ct))
            throw new InvalidOperationException($"El código '{request.Code}' ya existe.");

        var currency = new Currency
        {
            Code = request.Code.ToUpperInvariant(),
            Name = request.Name,
            RateToBase = request.RateToBase
        };

        return await _repository.CreateAsync(currency, ct);
    }
}
