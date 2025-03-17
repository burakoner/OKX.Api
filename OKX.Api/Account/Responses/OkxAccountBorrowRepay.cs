namespace OKX.Api.Account;

/// <summary>
/// OKX Account Manual Borrow-Repay Response
/// </summary>
public record OkxAccountBorrowRepay
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; } = "";

    /// <summary>
    /// Actual amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }
}
