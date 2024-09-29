namespace OKX.Api.Public;

/// <summary>
/// OKX VIP Interest Rate
/// </summary>
public class OkxPublicVipInterestRate
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

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
