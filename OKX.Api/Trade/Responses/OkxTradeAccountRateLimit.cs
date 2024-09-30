namespace OKX.Api.Trade;

/// <summary>
/// OKX Account Rate Limit
/// </summary>
public record OkxTradeAccountRateLimit
{
    /// <summary>
    /// Sub account fill ratio during the monitoring period
    /// Applicable for users with trading fee level >= VIP 5 and return "" for others
    /// For accounts with no trading volume during the monitoring period, return "0". For accounts with trading volume but no order count due to our counting logic, return "9999".
    /// </summary>
    [JsonProperty("fillRatio")]
    public decimal? FillRatio { get; set; }

    /// <summary>
    /// Master account aggregated fill ratio during the monitoring period
    /// Applicable for users with trading fee level >= VIP 5 and return "" for others
    /// For accounts with no trading volume during the monitoring period, return "0"
    /// </summary>
    [JsonProperty("mainFillRatio")]
    public decimal? MainFillRatio { get; set; }
    
    /// <summary>
    /// Current sub-account rate limit per two seconds
    /// </summary>
    [JsonProperty("accRateLimit")]
    public int AccountRateLimit { get; set; }

    /// <summary>
    /// Expected sub-account rate limit (per two seconds) in the next period
    /// Applicable for users with trading fee level >= VIP 5 and return "" for others
    /// </summary>
    [JsonProperty("nextAccRateLimit")]
    public int? NextAccountRateLimit { get; set; }

    /// <summary>
    /// Data update time
    /// For users with trading fee level >= VIP 5, the data will be generated at 08:00 am (UTC)
    /// For users with trading fee level &lt; VIP 5, return the current timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }
    
    /// <summary>
    /// Data update time
    /// For users with trading fee level >= VIP 5, the data will be generated at 08:00 am (UTC)
    /// For users with trading fee level &lt; VIP 5, return the current timestamp
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
