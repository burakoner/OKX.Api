namespace OKX.Api.Funding;

/// <summary>
/// OKX Asset Valuation
/// </summary>
public class OkxFundingAssetValuation
{
    /// <summary>
    /// Total Balance
    /// </summary>
    [JsonProperty("totalBal")]
    public decimal TotalBalance { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public OkxFundingAssetValuationDetails? Details { get; set; }
}

/// <summary>
/// OKX Asset Valuation Details
/// </summary>
public class OkxFundingAssetValuationDetails
{
    /// <summary>
    /// Funding
    /// </summary>
    [JsonProperty("funding")]
    public decimal Funding { get; set; }

    /// <summary>
    /// Trading
    /// </summary>
    [JsonProperty("trading")]
    public decimal Trading { get; set; }

    /// <summary>
    /// Classic
    /// </summary>
    [Obsolete("Deprecated")]
    [JsonProperty("classic")]
    public decimal Classic { get; set; }

    /// <summary>
    /// Earn
    /// </summary>
    [JsonProperty("earn")]
    public decimal Earn { get; set; }
}
