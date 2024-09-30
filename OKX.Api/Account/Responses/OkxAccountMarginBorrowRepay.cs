namespace OKX.Api.Account;

/// <summary>
/// Okx Margin Borrow-Repay
/// </summary>
public record OkxAccountMarginBorrowRepay
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// borrow repay
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; } = string.Empty;

    /// <summary>
    /// borrow/repay amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }
}
