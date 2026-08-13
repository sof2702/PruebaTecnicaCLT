using MediatR;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Infrastructure.Repositories;

namespace PruebaTecnicaCLT.Application.Currencies.Queries;

public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyDto>>
{
    private readonly ICurrencyRepository _repository;

    public GetCurrenciesHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<List<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken ct) =>
        await _repository.GetAllAsync(ct);
}
