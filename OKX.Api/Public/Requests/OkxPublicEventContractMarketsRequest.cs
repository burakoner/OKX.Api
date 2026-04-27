#pragma warning disable CS1591
namespace OKX.Api.Public;

/// <summary>
/// Event contract markets query request
/// </summary>
public record OkxPublicEventContractMarketsRequest
{
    public string? SeriesId { get; set; }
    public string? EventId { get; set; }
    public string? InstrumentId { get; set; }
    public OkxInstrumentState? State { get; set; }
    public int Limit { get; set; } = 100;
    public long? Before { get; set; }
    public long? After { get; set; }
}
#pragma warning restore CS1591
