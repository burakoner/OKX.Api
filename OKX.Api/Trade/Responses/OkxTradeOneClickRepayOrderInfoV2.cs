namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Order Info V2
/// </summary>
public record OkxTradeOneClickRepayOrderInfoV2
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Quantity to buy or sell
    /// </summary>
    [JsonProperty("sx")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Last filled price. If none is filled, it will return "".
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? LastFilledPrice { get; set; }

    /// <summary>
    /// Last filled quantity
    /// </summary>
    [JsonProperty("fillSx")]
    public decimal? LastFilledQuantity { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxTradeOneClickRepayOrderStatus Status { get; set; }

    /// <summary>
    /// Creation time for order, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Creation time for order
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
