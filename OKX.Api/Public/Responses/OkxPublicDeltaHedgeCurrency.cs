namespace OKX.Api.Public;

/// <summary>
/// Currencies sharing an underlying asset with the specified currency.
/// </summary>
public record OkxPublicDeltaHedgeCurrency
{
    /// <summary>
    /// Currency, e.g. ETH or AAPL.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Currencies sharing the same underlying asset and capable of forming a Delta hedge relationship
    /// with Currency, e.g. BETH for ETH. The relationship is symmetric.
    /// </summary>
    [JsonProperty("hedgeCcy")]
    public List<string> HedgeCurrencies { get; set; } = [];
}
