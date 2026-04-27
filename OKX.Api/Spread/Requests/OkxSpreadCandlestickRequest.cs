#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread candlestick query request
/// </summary>
public record OkxSpreadCandlestickRequest
{
    public string? SpreadId { get; set; }
    public string? Period { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
