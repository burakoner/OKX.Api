namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Lightning Deposit
/// </summary>
public class OkxFundingLightningDeposit
{
    /// <summary>
    /// Create Time
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Create Time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Invoice
    /// </summary>
    [JsonProperty("invoice")]
    public string Invoice { get; set; }
}