namespace PruebaTecnicaCLT.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal RateToBase { get; set; }
}
