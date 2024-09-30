namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Public Configuration
/// </summary>
public record OkxCopyTradingPublicConfiguration
{
    /// <summary>
    /// Maximum copy amount per order in USDT when you are using copy mode fixed_amount
    /// </summary>
    [JsonProperty("maxCopyAmt")]
    public decimal MaximumCopyAmount { get; set; }

    /// <summary>
    /// Minimum copy amount per order in USDT when you are using copy mode fixed_amount
    /// </summary>
    [JsonProperty("minCopyAmt")]
    public decimal MinimumCopyAmount { get; set; }

    /// <summary>
    /// Maximum copy total amount under the certain lead trader, the minimum is the same with minCopyAmt
    /// </summary>
    [JsonProperty("maxCopyTotalAmt")]
    public decimal MaximumCopyTotalAmount { get; set; }

    /// <summary>
    /// Minimum ratio per order when you are using copy mode ratio_copy
    /// </summary>
    [JsonProperty("minCopyRatio")]
    public decimal MinimumCopyRatio { get; set; }

    /// <summary>
    /// Maximum ratio per order when you are using copy mode ratio_copy
    /// </summary>
    [JsonProperty("maxCopyRatio")]
    public decimal MaximumCopyRatio { get; set; }

    /// <summary>
    /// Maximum ratio of taking profit per order, the minimum is 0
    /// </summary>
    [JsonProperty("maxTpRatio")]
    public decimal MaximumTakeProfitRatio { get; set; }

    /// <summary>
    /// Maximum ratio of stopping loss per order, the minimum is 0
    /// </summary>
    [JsonProperty("maxSlRatio")]
    public decimal MaximumStopLossRatio { get; set; }
}
