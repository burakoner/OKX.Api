namespace OKX.Api.Models.MarketData;

/// <summary>
/// OkxOptionTradeInfo
/// </summary>
public class OkxOptionTradeInfo
{
    /// <summary>
    /// The Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

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