namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxProfitSharingTotalUnrealized
/// </summary>
public record OkxCopyTradingProfitSharingTotalUnrealized
{
    /// <summary>
    /// The settlement time for the total unrealized profit sharing amount. Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("profitSharingTs")]
    public long Timestamp { get; set; }

    /// <summary>
    /// The settlement time for the total unrealized profit sharing amount.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Total unrealized profit sharing amount
    /// </summary>
    [JsonProperty("totalUnrealizedProfitSharingAmt")]
    public decimal TotalUnrealizedProfitSharingAmt { get; set; }
}
