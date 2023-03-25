namespace OKX.Api.Models.Trade;

public class OkxAlgoOrder
{
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    [JsonProperty("algoId")]
    public long? AlgoId { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }

    [JsonProperty("ordType"), JsonConverter(typeof(AlgoOrderTypeConverter))]
    public OkxAlgoOrderType OrderType { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("actualPx")]
    public decimal? ActualOrderPrice { get; set; }

    [JsonProperty("actualSz")]
    public decimal? ActualOrderQuantity { get; set; }

    [JsonProperty("ordPx")]
    public decimal? OrderPrice { get; set; }

    [JsonProperty("pxLimit")]
    public decimal? PriceLimit { get; set; }

    [JsonProperty("pxSpread")]
    public decimal? PriceRatio { get; set; }

    [JsonProperty("pxVar")]
    public decimal? PriceVariance { get; set; }

    [JsonProperty("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    [JsonProperty("szLimit")]
    public decimal? AverageQuantity { get; set; }

    [JsonProperty("timeInterval")]
    public long? TimeInterval { get; set; }

    [JsonProperty("tgtCcy"), JsonConverter(typeof(QuantityTypeConverter))]
    public OkxQuantityType? QuantityType { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(AlgoOrderStateConverter))]
    public OkxAlgoOrderState State { get; set; }

    [JsonProperty("actualSide"), JsonConverter(typeof(AlgoActualSideConverter))]
    public OkxAlgoActualSide? ActualSide { get; set; }
}
