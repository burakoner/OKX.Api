namespace OKX.Api.Models.GridTrading;

public class OkxGridAlgoOrder
{
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(GridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }
    
    [JsonProperty("state"), JsonConverter(typeof(GridAlgoOrderStatusConverter))]
    public OkxGridAlgoOrderStatus AlgoOrderStatus { get; set; }

    [JsonProperty("rebateTrans")]
    public IEnumerable<OkxGridRebateTransfer> RebateTransfers { get; set; }
    
    [JsonProperty("triggerParams")]
    public IEnumerable<OkxGridTriggerParameters> TriggerParameters { get; set; }

    [JsonProperty("maxPx")]
    public decimal? MaximumPrice { get; set; }

    [JsonProperty("minPx")]
    public decimal? MinimumPrice { get; set; }

    [JsonProperty("gridNum")]
    public decimal? Quantity { get; set; }

    [JsonProperty("runType"), JsonConverter(typeof(GridRunTypeConverter))]
    public OkxGridRunType? GridRunType { get; set; }

    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("arbitrageNum")]
    public decimal? ArbitrageNumber { get; set; }

    [JsonProperty("totalPnl")]
    public decimal? TotalProfitAndLoss { get; set; }

    [JsonProperty("pnlRatio")]
    public decimal? ProfitAndLossRatio { get; set; }
    
    [JsonProperty("investment")]
    public decimal? Investment { get; set; }
    
    [JsonProperty("gridProfit")]
    public decimal? GridProfit { get; set; }
    
    [JsonProperty("floatProfit")]
    public decimal? FloatProfit { get; set; }

    [JsonProperty("cancelType"), JsonConverter(typeof(GridCancelTypeConverter))]
    public OkxGridCancelType? CancelType { get; set; }

    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; }

    /*
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }
    */

    [JsonProperty("quoteSz")]
    public string QuoteSize { get; set; }

    [JsonProperty("baseSz")]
    public string BaseSize { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(GridContractDirectionConverter))]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    [JsonProperty("basePos")]
    public bool? BasePosition { get; set; }

    [JsonProperty("sz")]
    public string Size { get; set; }

    [JsonProperty("lever")]
    public string Leverage { get; set; }

    [JsonProperty("actualLever")]
    public string ActualLeverage { get; set; }

    [JsonProperty("liqPx")]
    public string LiquidationPrice { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    [JsonProperty("ordFrozen")]
    public string OrderFrozen { get; set; }

    [JsonProperty("availEq")]
    public string AvailableEquity { get; set; }
    
    [JsonProperty("tag")]
    internal string Tag { get; set; }
}

public class OkxGridRebateTransfer
{
    [JsonProperty("rebate")]
    public decimal Rebate { get; set; }

    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }
}

public class OkxGridTriggerParameters
{
    [JsonProperty("triggerAction"), JsonConverter(typeof(GridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    [JsonProperty("triggerStrategy"), JsonConverter(typeof(GridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    [JsonProperty("delaySeconds")]
    public int? DelaySeconds { get; set; }

    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? TriggerTime { get; set; }

    [JsonProperty("triggerType"), JsonConverter(typeof(GridAlgoTriggerTypeConverter))]
    public OkxGridAlgoTriggerType? TriggerType { get; set; }

    [JsonProperty("timeframe"), JsonConverter(typeof(GridAlgoTimeFrameConverter))]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    [JsonProperty("thold")]
    public decimal? Threshold { get; set; }

    [JsonProperty("triggerCond"), JsonConverter(typeof(GridAlgoTriggerConditionConverter))]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    [JsonProperty("timePeriod")]
    public int? TimePeriod { get; set; }

    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; }

    /*
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }
    */

}