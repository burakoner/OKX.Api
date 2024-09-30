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
    public string Currency { get; set; } = string.Empty;
}