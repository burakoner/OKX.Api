namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Cancel Request
/// </summary>
public record OkxTradeOrderCancelRequest
{
    /// <summary>
    /// Instrument ID code.
    /// If both instId and instIdCode are provided, instIdCode takes precedence.
    /// </summary>
    [JsonProperty("instIdCode")]
    public int? InstrumentIdCode { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [Obsolete("Will be deprecated on February 2026.")]
    [JsonProperty("instId")]
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