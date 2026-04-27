#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread trade query request
/// </summary>
public record OkxSpreadTradeQueryRequest
{
    public string? SpreadId { get; set; }
    public long? TradeId { get; set; }
    public long? OrderId { get; set; }
    public long? BeginId { get; set; }
    public long? EndId { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
