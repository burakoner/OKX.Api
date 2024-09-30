namespace OKX.Api.Funding;

/// <summary>
/// OKX Saving Balance
/// </summary>
public class OkxFundingSavingBalance
{
    /// <summary>
    /// Earnings
    /// </summary>
    [JsonProperty("earnings")]
    public decimal? Earnings { get; set; }

    /// <summary>
    /// Redempting Amount
    /// </summary>
    [JsonProperty("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }

    /// <summary>
    /// Lending Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? LendingRate { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Loan Amount
    /// </summary>
    [JsonProperty("loanAmt")]
    public decimal? LoanAmount { get; set; }

    /// <summary>
    /// Pending Amount
    /// </summary>
    [JsonProperty("pendingAmt")]
    public decimal? PendingAmount { get; set; }

}