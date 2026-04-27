#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread order query request
/// </summary>
public record OkxSpreadOrderQueryRequest
{
    public string? SpreadId { get; set; }
    public OkxTradeOrderType? Type { get; set; }
    public OkxTradeOrderState? State { get; set; }
    public long? BeginId { get; set; }
    public long? EndId { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
    public OkxInstrumentType? InstrumentType { get; set; }
    public string? InstrumentFamily { get; set; }
}
#pragma warning restore CS1591
