namespace OKX.Api.Trade;

/// <summary>
/// OkxTradeFill
/// </summary>
public record OkxTradeFill
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Filled quantity
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal FilledQuantity { get; set; }

    /// <summary>
    /// Last filled price
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal FilledPrice { get; set; }

    /// <summary>
    /// Trade direction
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Unix timestamp of the transaction's generation time in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// the transaction's generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// The last trade ID in the trades aggregation
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Liquidity taker or maker, T: taker M: maker
    /// </summary>
    [JsonProperty("execType")]
    public OkxTradeOrderRole LiquidityRole { get; set; }

    /// <summary>
    /// The count of trades aggregated
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// Sequence ID of the current message.
    /// </summary>
    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }
}
