namespace OKX.Api.Financial.FlexibleSimpleEarn;

/// <summary>
/// OKX Flexible Simple Earn Savings Balance
/// </summary>
public class OkxFlexibleSimpleEarnSavingsBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Currency earnings
    /// </summary>
    [JsonProperty("earnings")]
    public decimal Earnings { get; set; }

    /// <summary>
    /// Lending rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal LendingRate { get; set; }

    /// <summary>
    /// Lending amount
    /// </summary>
    [JsonProperty("loanAmt")]
    public decimal LendingAmount { get; set; }

    /// <summary>
    /// Pending amount
    /// </summary>
    [JsonProperty("pendingAmt")]
    public decimal PendingAmount { get; set; }

    /// <summary>
    /// Redempting amount (Deprecated)
    /// </summary>
    [Obsolete]
    [JsonProperty("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }
}
