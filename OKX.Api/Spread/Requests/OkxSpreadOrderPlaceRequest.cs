#pragma warning disable CS1591
namespace OKX.Api.Spread;

/// <summary>
/// Spread place order request
/// </summary>
public record OkxSpreadRestOrderPlaceRequest
{
    public string? SpreadId { get; set; }
    public OkxTradeOrderSide Side { get; set; }
    public OkxSpreadOrderType Type { get; set; }
    public decimal Size { get; set; }
    public decimal? Price { get; set; }
    public string? ClientOrderId { get; set; }
    public string? Tag { get; set; }
}
#pragma warning restore CS1591
