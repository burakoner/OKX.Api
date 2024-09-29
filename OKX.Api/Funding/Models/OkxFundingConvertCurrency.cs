namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Currency
/// </summary>
public class OkxFundingConvertCurrency
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Minimum amount to convert ( Deprecated )
    /// </summary>
    [Obsolete]
    [JsonProperty("min")]
    public decimal? Minimum { get; set; }

    /// <summary>
    /// Maximum amount to convert ( Deprecated )
    /// </summary>
    [Obsolete]
    [JsonProperty("max")]
    public decimal? Maximum { get; set; }
}