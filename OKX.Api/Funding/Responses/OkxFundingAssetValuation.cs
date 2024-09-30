namespace OKX.Api.Funding;

/// <summary>
/// OKX Asset Valuation
/// </summary>
public record OkxFundingAssetValuation
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