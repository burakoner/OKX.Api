namespace OKX.Api.Public;

/// <summary>
/// OKX Exchange Rate
/// </summary>
internal record OkxPublicExchangeRateContainer
{
    /// <summary>
    /// USD to CNY
    /// </summary>
    [JsonProperty("usdCny")]
    public decimal Payload { get; set; }
}
