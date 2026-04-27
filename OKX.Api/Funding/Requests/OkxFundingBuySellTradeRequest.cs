#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Buy/sell trade request
/// </summary>
public record OkxFundingBuySellTradeRequest
{
    public string? QuoteId { get; set; }
    public OkxTradeOrderSide Side { get; set; }
    public string? FromCurrency { get; set; }
    public string? ToCurrency { get; set; }
    public decimal RfqAmount { get; set; }
    public string? RfqCurrency { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ClientOrderId { get; set; }
}
#pragma warning restore CS1591
