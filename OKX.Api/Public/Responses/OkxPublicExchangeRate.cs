namespace OKX.Api.Public;

/// <summary>
/// OKX Exchange Rate
/// </summary>
public class OkxPublicExchangeRate
{
    /// <summary>
    /// USD to CNY
    /// </summary>
    [JsonProperty("usdCny")]
    public decimal UsdCny { get; set; }
}
