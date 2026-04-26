namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Product Info
/// </summary>
public record OkxFinancialProductInfo
{
    /// <summary>
    /// Fast redemption daily limit
    /// The master account and sub-accounts share the same limit
    /// </summary>
    [JsonProperty("fastRedemptionDailyLimit"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FastRedemptionDailyLimit { get; set; }

    /// <summary>
    /// Currently fast redemption max available amount
    /// </summary>
    [JsonProperty("fastRedemptionAvail"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FastRedemptionAvailable { get; set; }

    /// <summary>
    /// Latest APY
    /// </summary>
    [JsonProperty("rate"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Redemption days
    /// </summary>
    [JsonProperty("redemptDays"), JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? RedemptionDays { get; set; }

    /// <summary>
    /// Minimum amount
    /// </summary>
    [JsonProperty("minAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MinimumAmount { get; set; }
}
