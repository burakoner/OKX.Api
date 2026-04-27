namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial On Chain Earn Earning Data
/// </summary>
public record OkxFinancialOnChainEarnEarningData
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Earning type
    /// </summary>
    [JsonProperty("earningType")]
    public OkxFinancialOnChainEarnEarningType EarningType { get; set; }

    /// <summary>
    /// Earning amount
    /// </summary>
    [JsonProperty("earnings")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Earnings { get; set; }

    /// <summary>
    /// Realized earning amount
    /// </summary>
    [JsonProperty("realizedEarnings")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? RealizedEarnings { get; set; }
}
