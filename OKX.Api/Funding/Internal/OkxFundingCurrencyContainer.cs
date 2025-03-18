namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Currency
/// </summary>
internal record OkxFundingCurrencyContainer
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Payload { get; set; } = string.Empty;
}