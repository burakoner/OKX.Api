namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxProfitSharingUnrealized
/// </summary>
public class OkxCopyTradingProfitSharingUnrealized
{
    /// <summary>
    /// The currency of profit sharing. e.g. USDT
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Unrealized profit sharing amount.
    /// </summary>
    [JsonProperty("unrealizedProfitSharingAmt")]
    public decimal UnrealizedProfitSharingAmount { get; set; }

    /// <summary>
    /// Nickname of copy trader.
    /// </summary>
    [JsonProperty("nickName")]
    public string Nickname { get; set; } = string.Empty;
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Update time.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
