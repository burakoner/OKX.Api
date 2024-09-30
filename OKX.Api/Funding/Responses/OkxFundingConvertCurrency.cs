namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Currency
/// </summary>
internal record OkxFundingConvertCurrency
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Data { get; set; } = string.Empty;
}