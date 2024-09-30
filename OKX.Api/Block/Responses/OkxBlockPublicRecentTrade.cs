namespace OKX.Api.Block;

/// <summary>
/// OKX Block Trade
/// </summary>
public class OkxBlockPublicRecentTrade
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

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
    /// Side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Implied volatility. 
    /// Only applicable to OPTION
    /// </summary>
    [JsonProperty("fillVol")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Forward price
    /// Only applicable to OPTION
    /// </summary>
    [JsonProperty("fwdPx")]
    public decimal? ForwardPrice { get; set; }

    /// <summary>
    /// Index price
    /// Applicable to FUTURES, SWAP, OPTION
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// Mark price
    /// Applicable to FUTURES, SWAP, OPTION
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

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
