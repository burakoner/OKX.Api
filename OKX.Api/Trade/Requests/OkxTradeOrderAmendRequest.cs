namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Amend Request
/// </summary>
public record OkxTradeOrderAmendRequest
{
    /// <summary>
    /// Instrument ID code.
    /// Required for WebSocket amend order channels starting from 2026-04-07.
    /// If both instId and instIdCode are provided, instIdCode takes precedence.
    /// </summary>
    [JsonProperty("instIdCode", NullValueHandling = NullValueHandling.Ignore)]
    public long? InstrumentIdCode { get; set; }

    /// <summary>
    /// Instrument ID.
    /// Required for REST amend order requests.
    /// Deprecated and ignored by OKX for WebSocket amend order channels starting from 2026-04-07; use instIdCode there.
    /// </summary>
    [Obsolete("Deprecated and ignored by OKX for WebSocket amend order channels starting from 2026-04-07. Use InstrumentIdCode for WebSocket requests.")]
    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string? InstrumentId { get; set; }
    
    /// <summary>
    /// Whether OKX should cancel the original order if amendment fails. Default false preserves the original order.
    /// True requests cancellation on any amendment failure; do not assume that a 54051 rejection preserves it.
    /// </summary>
    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId", NullValueHandling = NullValueHandling.Ignore)]
    public string? RequestId { get; set; }

    /// <summary>
    /// New total target quantity, including any filled quantity; not the remaining unfilled quantity.
    /// When provided for an RPI/ELP maker order, OKX rechecks the minimum USD notional and rejects below-threshold amendments with 54051.
    /// A price-only amendment omitting newSz does not trigger this minimum-notional recheck.
    /// </summary>
    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewQuantity { get; set; }

    /// <summary>
    /// New price.
    /// </summary>
    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NewPrice { get; set; }

    /// <summary>
    /// Modify options orders using USD prices
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.
    /// </summary>
    [JsonProperty("newPxUsd", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NewPriceUsd { get; set; }

    /// <summary>
    /// Modify options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.
    /// </summary>
    [JsonProperty("newPxVol", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NewPriceVolatility { get; set; }

    /// <summary>
    /// Price Amend Type
    /// </summary>
    [JsonProperty("pxAmendType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradePriceAmendType? PriceAmendType { get; set; }

    /// <summary>
    /// Whether the amended order can access RPI liquidity. Default false.
    /// This value is not inherited from the original order and must be sent on each amend request.
    /// </summary>
    [JsonProperty("rpiTakerAccess", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RpiTakerAccess { get; set; }

    /// <summary>
    /// Whether an RPI maker price that violates the spacing rule may be rounded outward to the nearest placeable,
    /// non-crossing level. Default false. Effective only for rpi orders and ignored for OPTION and EVENTS.
    /// Cross/level checks and rounding use only the first visible opposite-side RPI; hidden RPI are excluded.
    /// OKX validates against the matching-engine snapshot on arrival, with the amended order still in the book.
    /// </summary>
    [JsonProperty("rpiPxRound", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RpiPriceRound { get; set; }

    /// <summary>
    /// Event contract speed bump flag.
    /// Required for non-post-only EVENTS amend requests.
    /// </summary>
    [JsonProperty("speedBump", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeEventSpeedBump? SpeedBump { get; set; }

    /// <summary>
    /// Attached TP/SL or trailing stop amendment information for REST requests.
    /// WebSocket serialization is retained for compatibility; current WS tables do not document this field.
    /// </summary>
    [JsonProperty("attachAlgoOrds", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<OkxTradeOrderAmendRequestAttachedAlgo>? AttachedAlgoOrders { get; set; }

    internal void Validate()
        => ValidatePrices(NewPrice, NewPriceUsd, NewPriceVolatility);

    internal static void ValidatePrices(decimal? newPrice, decimal? newPriceUsd, decimal? newPriceVolatility)
    {
        var priceCount = (newPrice.HasValue ? 1 : 0)
            + (newPriceUsd.HasValue ? 1 : 0)
            + (newPriceVolatility.HasValue ? 1 : 0);
        if (priceCount > 1)
            throw new ArgumentException("Only one of NewPrice, NewPriceUsd, or NewPriceVolatility can be provided.", nameof(NewPrice));
    }
}
