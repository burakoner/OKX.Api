#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Convert estimate quote request
/// </summary>
public record OkxFundingConvertEstimateQuoteRequest
{
    public string? BaseCurrency { get; set; }
    public string? QuoteCurrency { get; set; }
    public OkxTradeOrderSide Side { get; set; }
    public decimal RfqAmount { get; set; }
    public string? RfqCurrency { get; set; }
    public string? ClientOrderId { get; set; }
    public bool? UseLargeOrderConvert { get; set; }
}
#pragma warning restore CS1591
