namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Lightning Withdrawal
/// </summary>
public class OkxFundingLightningWithdrawal
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
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}