namespace OKX.Api.Block;

/// <summary>
/// OKX Block Leg Executed
/// </summary>
public record OkxBlockLegExecuted
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// The price the leg executed
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size of the leg in contracts or spot.
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }

    /// <summary>
    /// The direction of the leg from the Takers perspective. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }
    
    /// <summary>
    /// Last traded ID.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    /// <summary>
    /// The quote currency used for trading. Only applicable to SPOT.
    /// The default value is the quote currency of the instId, for example: for BTC-USD, the default is USD.
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string TradeQuoteCurrency { get; set; } = string.Empty;
}
