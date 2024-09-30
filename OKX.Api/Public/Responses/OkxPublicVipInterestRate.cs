namespace OKX.Api.Public;

/// <summary>
/// OKX VIP Interest Rate
/// </summary>
public record OkxPublicVipInterestRate
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Level List
    /// </summary>
    [JsonProperty("levelList")]
    public List<OkxPublicVipInterestRateLevel> LevelList { get; set; } = [];
}
