using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCLT.Application.Common.Dtos;
using PruebaTecnicaCLT.Domain.Entities;
using PruebaTecnicaCLT.Infrastructure.Data;

namespace PruebaTecnicaCLT.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _db;

    public CurrencyRepository(AppDbContext db) => _db = db;

    public async Task<List<CurrencyDto>> GetAllAsync(CancellationToken ct) =>
        await _db.Currencies
            .Select(c => new CurrencyDto(c.Id, c.Code, c.Name, c.RateToBase))
            .ToListAsync(ct);

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct) =>
        await _db.Currencies.AnyAsync(c => c.Code == code, ct);

    public async Task<CurrencyDto> CreateAsync(Currency currency, CancellationToken ct)
    {
        _db.Currencies.Add(currency);
        await _db.SaveChangesAsync(ct);
        return new CurrencyDto(currency.Id, currency.Code, currency.Name, currency.RateToBase);
    }

    public async Task<Currency?> GetByCodeAsync(string code, CancellationToken ct) =>
        await _db.Currencies.FirstOrDefaultAsync(c => c.Code == code, ct);
}
