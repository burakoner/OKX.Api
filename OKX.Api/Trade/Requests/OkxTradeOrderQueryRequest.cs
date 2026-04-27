#pragma warning disable CS1591
namespace OKX.Api.Trade;

/// <summary>
/// Historical order query request
/// </summary>
public record OkxTradeOrderQueryRequest
{
    public OkxInstrumentType? InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public string? InstrumentFamily { get; set; }
    public OkxTradeOrderType? OrderType { get; set; }
    public OkxTradeOrderState? State { get; set; }
    public OkxOrderCategory? Category { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
