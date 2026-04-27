#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Maximum loan amount query request
/// </summary>
public record OkxAccountMaximumLoanAmountRequest
{
    public OkxAccountMarginMode? MarginMode { get; set; }
    public string? InstrumentId { get; set; }
    public string? Currency { get; set; }
    public string? MarginCurrency { get; set; }
    public string? TradeQuoteCurrency { get; set; }
}
#pragma warning restore CS1591
