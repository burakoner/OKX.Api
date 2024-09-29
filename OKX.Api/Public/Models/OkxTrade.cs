namespace OKX.Api.Public;

/// <summary>
/// OKX Trade
/// </summary>
public class OkxTrade
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Trade Id
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeSideConverter))]
    public OkxTradeSide Side { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
