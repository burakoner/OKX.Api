namespace OKX.Api.Public;

/// <summary>
/// OKX Exchange Rate
/// </summary>
internal record OkxPublicExchangeRate
{
    /// <summary>
    /// USD to CNY
    /// </summary>
    [JsonProperty("usdCny")]
    public decimal Data { get; set; }
}
