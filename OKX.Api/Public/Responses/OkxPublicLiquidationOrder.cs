namespace OKX.Api.Public;

/// <summary>
/// OKX Liquidation Order
/// </summary>
public record OkxPublicLiquidationOrder
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Underlying. Applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicLiquidationOrderDetail> Details { get; set; } = [];
}

/// <summary>
/// OKX Liquidation Order Details
/// </summary>
public record OkxPublicLiquidationOrderDetail
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position mode side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Bankruptcy price. The price of the transaction with the system's liquidation account, only applicable to FUTURES/SWAP
    /// </summary>
    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    /// <summary>
    /// Bankruptcy loss
    /// </summary>
    [JsonProperty("bkLoss")]
    public decimal? BankruptcyLoss { get; set; }

    /// <summary>
    /// Quantity of liquidation, only applicable to MARGIN/FUTURES/SWAP.
    /// For MARGIN, the unit is base currency.
    /// For FUTURES/SWAP, the unit is contract.
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Size { get; set; }

    /// <summary>
    /// Liquidation currency, only applicable to MARGIN
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Liquidation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Liquidation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
