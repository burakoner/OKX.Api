namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Public Trade Data
/// </summary>
public class OkxSpreadPublicTrade
{
    /// <summary>
    /// Spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

    /// <summary>
    /// Trade ID
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
    /// Trade side of the taker.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide TakerSide { get; set; }

    /// <summary>
    /// Trade time, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}