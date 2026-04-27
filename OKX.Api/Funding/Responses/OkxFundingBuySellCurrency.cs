namespace OKX.Api.Funding;

/// <summary>
/// OKX buy/sell currency
/// </summary>
public record OkxFundingBuySellCurrency
{
    /// <summary>
    /// Supported fiat or crypto currency code.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}
