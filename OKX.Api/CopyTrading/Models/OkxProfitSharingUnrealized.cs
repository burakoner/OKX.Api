namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OkxProfitSharingUnrealized
/// </summary>
public class OkxProfitSharingUnrealized
{
    /// <summary>
    /// The currency of profit sharing. e.g. USDT
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Unrealized profit sharing amount.
    /// </summary>
    [JsonProperty("unrealizedProfitSharingAmt")]
    public decimal UnrealizedProfitSharingAmount { get; set; }

    /// <summary>
    /// Nickname of copy trader.
    /// </summary>
    [JsonProperty("nickName")]
    public string Nickname { get; set; }

    /// <summary>
    /// Update time.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
