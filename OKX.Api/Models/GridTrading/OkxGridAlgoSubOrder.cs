using OKX.Api.GridTrading.Converters;
using OKX.Api.GridTrading.Enums;
using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Models.GridTrading;

public class OkxGridAlgoSubOrder
{
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(GridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("groupId")]
    public string GroupId { get; set; }

    [JsonProperty("ordId")]
    public long? SubOrderId { get; set; }

    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    [JsonProperty("tdMode"), JsonConverter(typeof(OkxTradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ordType"), JsonConverter(typeof(OkxOrderTypeConverter))]
    public OkxOrderType OrderType { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(GridAlgoSubOrderStatusConverter))]
    public OkxGridAlgoSubOrderStatus AlgoSubOrderStatus { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    [JsonProperty("px")]
    public decimal? Price { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }

    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }

    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }
    
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }
    
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }
}