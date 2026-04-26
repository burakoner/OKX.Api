namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Cancel Request
/// </summary>
public record OkxTradeOrderCancelRequest
{
    /// <summary>
    /// Instrument ID code.
    /// Required for WebSocket cancel order channels starting from 2026-04-07.
    /// If both instId and instIdCode are provided, instIdCode takes precedence.
    /// </summary>
    [JsonProperty("instIdCode")]
    public long? InstrumentIdCode { get; set; }

    /// <summary>
    /// Instrument ID.
    /// Required for REST cancel order requests.
    /// Deprecated and ignored by OKX for WebSocket cancel order channels starting from 2026-04-07; use instIdCode there.
    /// </summary>
    [Obsolete("Deprecated and ignored by OKX for WebSocket cancel order channels starting from 2026-04-07. Use InstrumentIdCode for WebSocket requests.")]
    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string? InstrumentId { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }
}
