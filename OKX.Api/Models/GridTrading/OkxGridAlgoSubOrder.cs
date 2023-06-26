namespace OKX.Api.Models.GridTrading;

public class OkxGridAlgoSubOrder
{
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(GridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("groupId")]
    public string GroupId { get; set; }

    [JsonProperty("ordId")]
    public long? SubOrderId { get; set; }

    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
    public OkxOrderType OrderType { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(GridAlgoSubOrderStatusConverter))]
    public OkxGridAlgoSubOrderStatus AlgoSubOrderStatus { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
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

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("pnl")]
    public decimal? PnL { get; set; }
    
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }
    
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
    
    [JsonProperty("tag")]
    public string Tag { get; set; }
}