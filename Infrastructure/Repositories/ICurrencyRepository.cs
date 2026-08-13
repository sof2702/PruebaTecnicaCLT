using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public interface ICurrencyRepository
{
    Task<List<CurrencyDto>> GetAllAsync(CancellationToken ct);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct);
    Task<CurrencyDto> CreateAsync(Currency currency, CancellationToken ct);
    Task<Currency?> GetByCodeAsync(string code, CancellationToken ct);
}
