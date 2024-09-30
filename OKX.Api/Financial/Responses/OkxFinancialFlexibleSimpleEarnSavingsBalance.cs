namespace OKX.Api.Financial;

/// <summary>
/// OKX Flexible Simple Earn Savings Balance
/// </summary>
public record OkxFinancialFlexibleSimpleEarnSavingsBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

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
}
