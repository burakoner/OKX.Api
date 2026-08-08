namespace OKX.Api.Account;

/// <summary>
/// Demo account balance adjustment result.
/// </summary>
public record OkxAccountDemoBalanceAdjustment
{
    /// <summary>
    /// Remaining daily increase quota. Reduce requests do not consume this quota.
    /// </summary>
    [JsonProperty("remainCnt")]
    [JsonConverter(typeof(IntAsStringConverter))]
    public int RemainingIncreaseCount { get; set; }

    /// <summary>
    /// Total daily increase quota.
    /// </summary>
    [JsonProperty("totalCnt")]
    [JsonConverter(typeof(IntAsStringConverter))]
    public int TotalIncreaseCount { get; set; }

    /// <summary>
    /// Per-currency adjustment results.
    /// </summary>
    [JsonProperty("details")]
    public List<OkxAccountDemoBalanceAdjustmentDetail> Details { get; set; } = [];
}

/// <summary>
/// Per-currency demo account balance adjustment result.
/// </summary>
public record OkxAccountDemoBalanceAdjustmentDetail
{
    /// <summary>
    /// Currency.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Adjustment amount applied.
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// Balance after the adjustment.
    /// </summary>
    [JsonProperty("bal")]
    [JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Balance { get; set; }
}
