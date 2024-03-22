namespace OKX.Api.Account.Models;

/// <summary>
/// OkxWithdrawalAmount
/// </summary>
public class OkxWithdrawalAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Max withdrawal (excluding borrowed assets under Multi-currency margin/Portfolio margin)
    /// </summary>
    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawalExcludingBorrowedAssets { get; set; }

    /// <summary>
    /// Max withdrawal (including borrowed assets under Multi-currency margin/Portfolio margin)
    /// </summary>
    [JsonProperty("maxWdEx")]
    public decimal? MaximumWithdrawalIncludingBorrowedAssets { get; set; }

    /// <summary>
    /// Max withdrawal under Spot-Derivatives risk offset mode (excluding borrowed assets under Portfolio margin)
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetMaxWd")]
    public decimal? SpotMaximumWithdrawalExcludingBorrowedAssets { get; set; }

    /// <summary>
    /// Max withdrawal under Spot-Derivatives risk offset mode (including borrowed assets under Portfolio margin)
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetMaxWdEx")]
    public decimal? SpotMaximumWithdrawalIncludingBorrowedAssets { get; set; }
}
