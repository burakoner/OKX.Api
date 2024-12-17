namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Trade Leg
/// </summary>
public record OkxSpreadTradeLeg
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
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Filled amount of the contract
    /// Only applicable to contracts, return "" for spot
        /// </summary>
        [JsonProperty("szCont")]
    public decimal? ContractQuantity { get; set; }

    /// <summary>
    /// The direction of the leg. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Last filled profit and loss, applicable to orders which have a trade and aim to close position. It always is 0 in other conditions
    /// </summary>
    [JsonProperty("fillPnl")]
    public decimal? LastFilledPnl { get; set; }

    /// <summary>
    /// Fee. Negative number represents the user transaction fee charged by the platform. Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal FeeQuantity { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Traded ID in the OKX orderbook.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }
}