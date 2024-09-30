namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Volume
/// </summary>
public class OkxFinancialFixedSimpleEarnLendingVolume
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Fixed term for lending order
    /// 30D: 30 days
    /// </summary>
    [JsonProperty("term")]
    public int Term { get; set; }

    /// <summary>
    /// Lending APR of the lower range, e.g. 0.0100 represent 1%
    /// </summary>
    [JsonProperty("rateRangeFrom")]
    public decimal RateRangeFrom { get; set; }

    /// <summary>
    /// Lending APR of the higher range, e.g. 0.0100 represent 1%
    /// </summary>
    [JsonProperty("rateRangeTo")]
    public decimal RateRangeTo { get; set; }

    /// <summary>
    /// Lending volume pending to match
    /// </summary>
    [JsonProperty("pendingVol")]
    public decimal PendingVolume { get; set; }
}