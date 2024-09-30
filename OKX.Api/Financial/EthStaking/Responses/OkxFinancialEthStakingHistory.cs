﻿namespace OKX.Api.Financial.EthStaking;

/// <summary>
/// OKX Financial Eth Staking Purchase-Redeem History
/// </summary>
public class OkxFinancialEthStakingHistory
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxFinancialEthStakingTypeConverter))]
    public OkxFinancialEthStakingType Type { get; set; }

    /// <summary>
    /// Purchase/Redeem amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(OkxFinancialEthStakingStatusConverter))]
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