#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Buy/sell quote request
/// </summary>
public record OkxFundingBuySellQuoteRequest
{
    public OkxTradeOrderSide Side { get; set; }
    public string? FromCurrency { get; set; }
    public string? ToCurrency { get; set; }
    public decimal RfqAmount { get; set; }
    public string? RfqCurrency { get; set; }
}
#pragma warning restore CS1591
