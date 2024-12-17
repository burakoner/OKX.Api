namespace OKX.Api.Public;

/// <summary>
/// OKX Ticker
/// </summary>
public record OkxPublicTicker
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Last Price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Last Size
    /// </summary>
    [JsonProperty("lastSz")]
    public decimal? LastSize { get; set; }

    /// <summary>
    /// Ask Price
    /// </summary>
    [JsonProperty("askPx")]
    public decimal? AskPrice { get; set; }

    /// <summary>
    /// Ask Size
    /// </summary>
    [JsonProperty("askSz")]
    public decimal? AskSize { get; set; }

    /// <summary>
    /// Bid Price
    /// </summary>
    [JsonProperty("bidPx")]
    public decimal? BidPrice { get; set; }

    /// <summary>
    /// Bid Size
    /// </summary>
    [JsonProperty("bidSz")]
    public decimal? BidSize { get; set; }

    /// <summary>
    /// Open Price
    /// </summary>
    [JsonProperty("open24h")]
    public decimal? Open { get; set; }

    /// <summary>
    /// High
    /// </summary>
    [JsonProperty("high24h")]
    public decimal? High { get; set; }

    /// <summary>
    /// Low
    /// </summary>
    [JsonProperty("low24h")]
    public decimal? Low { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal VolumeCurrency { get; set; }

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Open Price UTC 0
    /// </summary>
    [JsonProperty("sodUtc0")]
    public decimal OpenPriceUtc0 { get; set; }

    /// <summary>
    /// Open Price UTC 8
    /// </summary>
    [JsonProperty("sodUtc8")]
    public decimal OpenPriceUtc8 { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
