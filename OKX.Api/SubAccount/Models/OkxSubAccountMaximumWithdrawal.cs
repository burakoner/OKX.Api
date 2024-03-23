namespace OKX.Api.SubAccount.Models;

/// <summary>
/// SubAccount Maximum Withdrawal
/// </summary>
public class OkxSubAccountMaximumWithdrawal
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Max withdrawal (excluding borrowed assets under Multi-currency margin)
    /// </summary>
    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawal { get; set; }
    
    /// <summary>
    /// Max withdrawal (including borrowed assets under Multi-currency margin)
    /// </summary>
    [JsonProperty("maxWdEx")]
    public decimal? MaximumWithdrawalExtended { get; set; }

    /// <summary>
    /// Max withdrawal under Spot-Derivatives risk offset mode (excluding borrowed assets under Portfolio margin)
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetMaxWd")]
    public decimal? SpotOffsetMaximumWithdrawal { get; set; }

    /// <summary>
    /// Max withdrawal under Spot-Derivatives risk offset mode (including borrowed assets under Portfolio margin)
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetMaxWdEx")]
    public decimal? SpotOffsetMaximumWithdrawalExtended { get; set; }
}
