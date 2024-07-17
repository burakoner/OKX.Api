using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;
using OKX.Api.GridTrading.Converters;
using OKX.Api.GridTrading.Enums;
using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trading.Converters;
using OKX.Api.Trading.Enums;

namespace OKX.Api.GridTrading.Models;

/// <summary>
/// OKX Grid Algo Sub Order
/// </summary>
public class OkxGridAlgoSubOrder
{
    /// <summary>
    /// Algo order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Algo client order ID
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
    public string InstrumentId { get; set; }

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Group ID
    /// </summary>
    [JsonProperty("groupId")]
    public string GroupId { get; set; }

    /// <summary>
    /// Sub order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long? SubOrderId { get; set; }

    /// <summary>
    /// Created time of sub order
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Created time of sub order
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Updated time of sub order
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Updated time of sub order
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(OkxTradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(OkxOrderTypeConverter))]
    public OkxOrderType OrderType { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Algo sub order status
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxGridAlgoSubOrderStatusConverter))]
    public OkxGridAlgoSubOrderStatus AlgoSubOrderStatus { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Rebate
    /// </summary>
    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Rebate currency
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}