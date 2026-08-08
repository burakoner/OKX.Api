namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Limits
/// </summary>
public record OkxFinancialOkusdLimits
{
    /// <summary>
    /// Subscription limit information
    /// </summary>
    [JsonProperty("subLimit")]
    public OkxFinancialOkusdSubscriptionLimit SubscriptionLimit { get; set; } = new();

    /// <summary>
    /// Fast redemption limit information
    /// </summary>
    [JsonProperty("fastRedeemLimit")]
    public OkxFinancialOkusdRedemptionLimit FastRedemptionLimit { get; set; } = new();

    /// <summary>
    /// Standard redemption limit information
    /// </summary>
    [JsonProperty("stdRedeemLimit")]
    public OkxFinancialOkusdRedemptionLimit StandardRedemptionLimit { get; set; } = new();

    /// <summary>
    /// Server timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Server time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OKUSD Subscription Limit
/// </summary>
public record OkxFinancialOkusdSubscriptionLimit
{
    /// <summary>
    /// Maximum subscribable amount for today in USDT
    /// </summary>
    [JsonProperty("maxSubAmt")]
    public decimal MaximumSubscriptionAmount { get; set; }

    /// <summary>
    /// Personal daily subscription limit in USDT
    /// </summary>
    [JsonProperty("personalDailyLimit")]
    public decimal PersonalDailyLimit { get; set; }

    /// <summary>
    /// Personal subscription amount already used today in USDT
    /// </summary>
    [JsonProperty("personalUsedAmt")]
    public decimal PersonalUsedAmount { get; set; }

    /// <summary>
    /// Platform-wide daily subscription limit in USDT
    /// </summary>
    [JsonProperty("platformDailyLimit")]
    public decimal PlatformDailyLimit { get; set; }

    /// <summary>
    /// Platform-wide subscription amount already used today in USDT
    /// </summary>
    [JsonProperty("platformUsedAmt")]
    public decimal PlatformUsedAmount { get; set; }
}

/// <summary>
/// OKUSD Redemption Limit
/// </summary>
public record OkxFinancialOkusdRedemptionLimit
{
    /// <summary>
    /// Personal daily redemption limit in OKUSD
    /// </summary>
    [JsonProperty("personalDailyLimit")]
    public decimal PersonalDailyLimit { get; set; }

    /// <summary>
    /// Personal redemption amount already used today in OKUSD
    /// </summary>
    [JsonProperty("personalUsedAmt")]
    public decimal PersonalUsedAmount { get; set; }

    /// <summary>
    /// Platform-wide daily redemption limit in OKUSD
    /// </summary>
    [JsonProperty("platformDailyLimit")]
    public decimal PlatformDailyLimit { get; set; }

    /// <summary>
    /// Platform-wide redemption amount already used today in OKUSD
    /// </summary>
    [JsonProperty("platformUsedAmt")]
    public decimal PlatformUsedAmount { get; set; }

    /// <summary>
    /// Redemption fee rate
    /// </summary>
    [JsonProperty("feeRate")]
    public decimal FeeRate { get; set; }
}