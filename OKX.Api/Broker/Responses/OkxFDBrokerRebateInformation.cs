namespace OKX.Api.Broker;

/// <summary>
/// FD broker rebate information
/// </summary>
public record OkxFDBrokerRebateInformation
{
    /// <summary>
    /// Broker rebate eligibility status.
    /// </summary>
    [JsonProperty("type")]
    public OkxFDBrokerRebateStatus Status { get; set; }

    /// <summary>
    /// FD broker code.
    /// </summary>
    [JsonProperty("brokerCode")]
    public string BrokerCode { get; set; } = string.Empty;

    /// <summary>
    /// Whether there is an affiliated rebate.
    /// </summary>
    [JsonProperty("affiliated")]
    public bool IsAffiliated { get; set; }

    /// <summary>
    /// Commission rebate ratio for the client.
    /// </summary>
    [JsonProperty("clientRebateRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ClientRebateRatio { get; set; }

    /// <summary>
    /// Account monthly rebate amount. Only applicable to VIP5 and VIP6.
    /// </summary>
    [JsonProperty("lastRebate"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? LastRebate { get; set; }
}
