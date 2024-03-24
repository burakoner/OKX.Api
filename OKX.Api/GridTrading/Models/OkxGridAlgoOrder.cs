using OKX.Api.GridTrading.Converters;
using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Models;

/// <summary>
/// OKX Grid Algo Order
/// </summary>
public class OkxGridAlgoOrder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Algo order state
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxGridAlgoOrderStatusConverter))]
    public OkxGridAlgoOrderStatus AlgoOrderStatus { get; set; }

    /// <summary>
    /// Rebate transfer info
    /// </summary>
    [JsonProperty("rebateTrans")]
    public List<OkxGridRebateTransfer> RebateTransfers { get; set; }

    /// <summary>
    /// Trigger Parameters
    /// </summary>
    [JsonProperty("triggerParams")]
    public List<OkxGridTriggerParameters> TriggerParameters { get; set; }

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
    [JsonProperty("runType"), JsonConverter(typeof(OkxGridRunTypeConverter))]
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
    /// Total P&L
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalProfitLoss { get; set; }

    /// <summary>
    /// P&L ratio
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
    /// Variable P&L
    /// </summary>
    [JsonProperty("floatProfit")]
    public decimal? FloatProfit { get; set; }

    /// <summary>
    /// Algo order stop reason
    /// </summary>
    [JsonProperty("cancelType"), JsonConverter(typeof(OkxGridCancelTypeConverter))]
    public OkxGridCancelType? CancelType { get; set; }

    /// <summary>
    /// Actual Stop type
    /// Spot/Moon grid 1: Sell base currency 2: Keep base currency
    /// Contract grid 1: Market Close All positions 2: Keep positions
    /// </summary>
    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; }

    /*
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }
    */

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
    [JsonProperty("direction"), JsonConverter(typeof(OkxGridContractDirectionConverter))]
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
    public decimal? Leverage { get; set; }

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
    public string Underlying { get; set; }

    /// <summary>
    /// Instrument family
    /// Only applicable to FUTURES/SWAP/OPTION
    /// Only applicable to contract grid
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

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
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }

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
    [JsonProperty("copyType"), JsonConverter(typeof(OkxProfitSharingOrderTypeConverter))]
    public OkxProfitSharingOrderType ProfitSharingOrderType { get; set; }
}

/// <summary>
/// Rebate transfer info
/// </summary>
public class OkxGridRebateTransfer
{
    /// <summary>
    /// Rebate amount
    /// </summary>
    [JsonProperty("rebate")]
    public decimal Rebate { get; set; }

    /// <summary>
    /// Rebate currency
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }
}

/// <summary>
/// Trigger Parameters
/// </summary>
public class OkxGridTriggerParameters
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonProperty("triggerAction"), JsonConverter(typeof(OkxGridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy
    /// </summary>
    [JsonProperty("triggerStrategy"), JsonConverter(typeof(OkxGridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Delay seconds after action triggered
    /// </summary>
    [JsonProperty("delaySeconds")]
    public int? DelaySeconds { get; set; }

    /// <summary>
    /// Actual action triggered time, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    /// <summary>
    /// Actual action triggered type
    /// </summary>
    [JsonProperty("triggerType"), JsonConverter(typeof(OkxGridAlgoTriggerTypeConverter))]
    public OkxGridAlgoTriggerType? TriggerType { get; set; }

    /// <summary>
    /// K-line type
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("timeframe"), JsonConverter(typeof(OkxGridAlgoTimeFrameConverter))]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// Threshold
    /// The value should be an integer between 1 to 100
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("thold")]
    public decimal? Threshold { get; set; }

    /// <summary>
    /// Trigger condition
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("triggerCond"), JsonConverter(typeof(OkxGridAlgoTriggerConditionConverter))]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// Time Period
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("timePeriod")]
    public int? TimePeriod { get; set; }

    /// <summary>
    /// Trigger Price
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Stop type
    /// Spot grid 1: Sell base currency 2: Keep base currency
    /// Contract grid 1: Market Close All positions 2: Keep positions
    /// This field is only valid when triggerAction is stop
    /// </summary>
    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; }

    /*
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }
    */
}
