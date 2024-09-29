namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Offer
/// </summary>
public class OkxFinancialFixedSimpleEarnLendingOffer
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    /// <summary>
    /// Fixed term for lending order
    /// 30D: 30 days
    /// </summary>
    [JsonProperty("term")]
    public int Term { get; set; }

    /// <summary>
    /// Latest lending APY, in decimal.
    /// e.g. 0.01 represent 1%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
    
    /// <summary>
    /// Minimum lending amount
    /// </summary>
    [JsonProperty("minLend")]
    public decimal MinimumLendingAmount { get; set; }

    /// <summary>
    /// Lending quota
    /// </summary>
    [JsonProperty("lendQuota")]
    public decimal LendingQuota { get; set; }
}