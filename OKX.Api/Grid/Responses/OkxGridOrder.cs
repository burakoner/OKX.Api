namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Order
/// </summary>
public record OkxGridOrder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Algo order updated time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Algo order state
    /// </summary>
    [JsonProperty("state")]
    public OkxGridAlgoOrderStatus AlgoOrderStatus { get; set; }

    /// <summary>
    /// Rebate transfer info
    /// </summary>
    [JsonProperty("rebateTrans")]
    public List<OkxGridOrderRebateTransfer> RebateTransfers { get; set; } = [];

    /// <summary>
    /// Trigger Parameters
    /// </summary>
    [JsonProperty("triggerParams")]
    public List<OkxGridTriggerParameters> TriggerParameters { get; set; } = [];

    /// <summary>
    /// Upper price of price range
    /// </summary>
    [JsonProperty("maxPx")]
    public decimal? MaximumPrice { get; set; }

    /// <summary>
    /// Lower price of price range
    /// </summary>
    [JsonProperty("minPx")]
    public decimal? MinimumPrice { get; set; }

    /// <summary>
    /// Grid quantity
    /// </summary>
    [JsonProperty("gridNum")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Grid type
    /// </summary>
    [JsonProperty("runType")]
    public OkxGridRunType? GridRunType { get; set; }

    /// <summary>
    /// Take-profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// The number of arbitrages executed
    /// </summary>
    [JsonProperty("arbitrageNum")]
    public decimal? ArbitrageNumber { get; set; }

    /// <summary>
    /// Total P&amp;L
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalProfitLoss { get; set; }

    /// <summary>
    /// P&amp;L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal? ProfitLossRatio { get; set; }

    /// <summary>
    /// Investment amount
    /// Spot grid investment amount calculated on quote currency
    /// </summary>
    [JsonProperty("investment")]
    public decimal? Investment { get; set; }

    /// <summary>
    /// Grid profit
    /// </summary>
    [JsonProperty("gridProfit")]
    public decimal? GridProfit { get; set; }

    /// <summary>
    /// Variable P&amp;L
    /// </summary>
    [JsonProperty("floatProfit")]
    public decimal? FloatProfit { get; set; }

    /// <summary>
    /// Algo order stop reason
    /// </summary>
    [JsonProperty("cancelType")]
    public OkxGridCancelType? CancelType { get; set; }

    /// <summary>
    /// Actual Stop type
    /// Spot/Moon grid 1: Sell base currency 2: Keep base currency
    /// Contract grid 1: Market Close All positions 2: Keep positions
    /// </summary>
    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency investment amount
    /// Only applicable to Spot grid/Moon grid
    /// </summary>
    [JsonProperty("quoteSz")]
    public decimal? QuoteSize { get; set; }

    /// <summary>
    /// Base currency investment amount
    /// Only applicable to Spot grid
    /// </summary>
    [JsonProperty("baseSz")]
    public decimal? BaseSize { get; set; }

    /// <summary>
    /// Contract grid type
    /// long,short,neutral
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("direction")]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    /// <summary>
    /// Whether or not to open a position when the strategy is activated
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("basePos")]
    public bool? BasePosition { get; set; }

    /// <summary>
    /// Used margin based on USDT
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Size { get; set; }

    /// <summary>
    /// Leverage
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("lever")]
    public int? Leverage { get; set; }

    /// <summary>
    /// Actual Leverage
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("actualLever")]
    public decimal? ActualLeverage { get; set; }

    /// <summary>
    /// Estimated liquidation price
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Underlying
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// Only applicable to FUTURES/SWAP/OPTION
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Margin used by pending orders
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Available margin
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Profit sharing ratio
    /// Value range [0, 0.3]
    /// If it is a normal order(neither copy order nor lead order), this field returns ""
    /// </summary>
    [JsonProperty("profitSharingRatio")]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Profit sharing order type
    /// </summary>
    [JsonProperty("copyType")]
    public OkxProfitSharingOrderType ProfitSharingOrderType { get; set; }

    /// <summary>
    /// Take profit ratio, 0.1 represents 10%
    /// </summary>
    [JsonProperty("tpRatio")]
    public decimal? TakeProfitRatio { get; set; }

    /// <summary>
    /// Stop loss ratio, 0.1 represents 10%
    /// </summary>
    [JsonProperty("slRatio")]
    public decimal? StopLossRatio { get; set; }

    /// <summary>
    /// Accumulated fee. Only applicable to contract grid, or it will be ""
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Accumulated funding fee. Only applicable to contract grid, or it will be ""
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal? FundingFee { get; set; }
}
