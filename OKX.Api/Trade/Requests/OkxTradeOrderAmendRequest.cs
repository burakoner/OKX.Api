namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Amend Request
/// </summary>
public record OkxTradeOrderAmendRequest
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Cancel on fail
    /// </summary>
    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// New quantity.
    /// </summary>
    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewQuantity { get; set; }

    /// <summary>
    /// New price.
    /// </summary>
    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPrice { get; set; }

    /// <summary>
    /// Modify options orders using USD prices
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.
    /// </summary>
    [JsonProperty("newPxUsd", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPriceUsd { get; set; }

    /// <summary>
    /// Modify options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.
    /// </summary>
    [JsonProperty("newPxVol", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPriceVolatility { get; set; }

    /// <summary>
    /// TP/SL information attached when placing order
    /// </summary>
    [JsonProperty("attachAlgoOrds", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<OkxTradeOrderAmendAttachedAlgoRequest>? AttachedAlgoOrders { get; set; }
}