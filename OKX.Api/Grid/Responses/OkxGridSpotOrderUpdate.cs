namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Spot Order Update
/// </summary>
public record OkxGridSpotOrderUpdate
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
    /// The number of trades executed
    /// </summary>
    [JsonProperty("tradeNum")]
    public decimal? TradeNumber { get; set; }

    /// <summary>
    /// The number of arbitrages executed
    /// </summary>
    [JsonProperty("arbitrageNum")]
    public decimal? ArbitrageNumber { get; set; }

    /// <summary>
    ///	Amount per grid
    /// </summary>
    [JsonProperty("singleAmt")]
    public decimal? AmountPerGrid { get; set; }

    /// <summary>
    /// Estimated minimum Profit margin per grid
    /// </summary>
    [JsonProperty("perMinProfitRate")]
    public decimal? EstimatedMinimumProfitMarginPerGrid { get; set; }

    /// <summary>
    /// Estimated maximum Profit margin per grid
    /// </summary>
    [JsonProperty("perMaxProfitRate")]
    public decimal? EstimatedMaximumProfitMarginPerGrid { get; set; }

    /// <summary>
    /// Price at launch
    /// </summary>
    [JsonProperty("runPx")]
    public decimal? PriceAtLaunch { get; set; }

    /// <summary>
    /// Total P&amp;L
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalProfitAndLoss { get; set; }

    /// <summary>
    /// P&amp;L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal? ProfitAndLossRatio { get; set; }

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
    /// Total annualized rate
    /// </summary>
    [JsonProperty("totalAnnualizedRate")]
    public decimal? TotalAnnualizedRate { get; set; }

    /// <summary>
    /// Grid annualized rate
    /// </summary>
    [JsonProperty("annualizedRate")]
    public decimal? GridAnnualizedRate { get; set; }

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
    /// Assets of quote currency currently held
    /// Only applicable to Spot grid
    /// </summary>
    [JsonProperty("curQuoteSz")]
    public decimal? CurrentQuoteSize { get; set; }

    /// <summary>
    /// Assets of base currency currently held
    /// Only applicable to Spot grid
    /// </summary>
    [JsonProperty("curBaseSz")]
    public decimal? CurrentBaseSize { get; set; }

    /// <summary>
    /// Current available profit based on quote currency
    /// Only applicable to Spot grid
        /// </summary>
        [JsonProperty("profit")]
    public decimal? Profit { get; set; }
    
    /// <summary>
    /// Stop result
    /// 0: default, 1: Successful selling of currency at market price, -1: Failed to sell currency at market price
    /// Only applicable to Spot grid
    /// </summary>
    [JsonProperty("stopResult")]
    public string StopResult { get; set; } = string.Empty;

    /// <summary>
    /// Total count of pending sub orders
    /// </summary>
    [JsonProperty("activeOrdNum")]
    public decimal? PendingSubOrders { get; set; }

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
    /// Push time of algo grid information, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("pTime")]
    public long? PushTimestamp { get; set; }

    /// <summary>
    /// Push time of algo grid information
    /// </summary>
    [JsonIgnore]
    public DateTime? PushTime => PushTimestamp?.ConvertFromMilliseconds();

}
