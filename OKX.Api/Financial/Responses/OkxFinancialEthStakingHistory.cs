namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Eth Staking Purchase-Redeem History
/// </summary>
public record OkxFinancialEthStakingHistory
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxFinancialEthStakingType Type { get; set; }

    /// <summary>
    /// Purchase/Redeem amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Redeeming amount
    /// </summary>
    [JsonProperty("redeemingAmt")]
    public decimal RedeemingAmount { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public OkxFinancialEthStakingStatus Status { get; set; }

    /// <summary>
    /// Request time of make purchase/redeem, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("requestTime")]
    public long RequestTimestamp { get; set; }

    /// <summary>
    /// Request time of make purchase/redeem
    /// </summary>
    [JsonIgnore]
    public DateTime RequestTime => RequestTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Completed time of redeem settlement, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("completedTime")]
    public long? CompletedTimestamp { get; set; }

    /// <summary>
    /// Completed time of redeem settlement
    /// </summary>
    [JsonIgnore]
    public DateTime? CompletedTime => CompletedTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Estimated completed time of redeem settlement, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("estCompletedTime")]
    public long? EstimatedCompletedTimestamp { get; set; }

    /// <summary>
    /// Estimated completed time of redeem settlement
    /// </summary>
    [JsonIgnore]
    public DateTime? EstimatedCompletedTime => EstimatedCompletedTimestamp?.ConvertFromMilliseconds();
}
