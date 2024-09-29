namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxProfitSharingUnrealized
/// </summary>
public class OkxCopyTradingProfitSharingUnrealized
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
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; }

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
