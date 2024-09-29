namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxProfitSharingDetails
/// </summary>
public class OkxCopyTradingProfitSharingDetails
{
    /// <summary>
    /// The currency of profit sharing.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Profit sharing amount. Default is 0.
    /// </summary>
    [JsonProperty("profitSharingAmt")]
    public decimal ProfitSharingAmount { get; set; }

    /// <summary>
    /// Nickname of copy trader.
    /// </summary>
    [JsonProperty("nickName")]
    public string Nickname { get; set; }

    /// <summary>
    /// Profit sharing ID.
    /// </summary>
    [JsonProperty("profitSharingId")]
    public long ProfitSharingId { get; set; }
    
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
    /// Profit sharing time.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Profit sharing time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
