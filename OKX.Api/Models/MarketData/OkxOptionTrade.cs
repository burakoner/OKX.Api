namespace OKX.Api.Models.MarketData;

/// <summary>
/// OkxOptionTrade
/// </summary>
public class OkxOptionTrade
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// Trade price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Trade quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Trade side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(TradeSideConverter))]
    public OkxTradeSide Side { get; set; }

    /// <summary>
    /// Option type, C: Call P: Put
    /// </summary>
    [JsonProperty("optType"), JsonConverter(typeof(OptionTypeConverter))]
    public OkxOptionType OptionType { get; set; }

    /// <summary>
    /// Implied volatility while trading (Correspond to trade price)
    /// </summary>
    [JsonProperty("fillVol")]
    public decimal FillVolume { get; set; }

    /// <summary>
    /// Forward price while trading
    /// </summary>
    [JsonProperty("fwdPx")]
    public decimal ForwardPrice { get; set; }

    /// <summary>
    /// Index price while trading
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Mark price while trading
    /// </summary>
    [JsonProperty("markPx")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Trade time, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}