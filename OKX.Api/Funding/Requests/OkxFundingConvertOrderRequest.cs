#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Convert trade request
/// </summary>
public record OkxFundingConvertOrderRequest
{
    public string? QuoteId { get; set; }
    public string? BaseCurrency { get; set; }
    public string? QuoteCurrency { get; set; }
    public OkxTradeOrderSide Side { get; set; }
    public decimal Amount { get; set; }
    public string? AmountCurrency { get; set; }
    public string? ClientOrderId { get; set; }
    public bool? UseLargeOrderConvert { get; set; }
}
#pragma warning restore CS1591
