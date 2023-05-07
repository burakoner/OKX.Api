namespace OKX.Api.Models.Account;

public class OkxPosition
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("pos")]
    public decimal? PositionsQuantity { get; set; }

    [JsonProperty("baseBal")]
    public decimal? BaseCurrencyBalance { get; set; }

    [JsonProperty("quoteBal")]
    public decimal? QuoteCurrencyBalance { get; set; }

    [JsonProperty("baseBorrowed")]
    public decimal? BaseBorrowed { get; set; }

    [JsonProperty("baseInterest")]
    public decimal? BaseInterest { get; set; }

    [JsonProperty("quoteBorrowed")]
    public decimal? QuoteBorrowed { get; set; }

    [JsonProperty("quoteInterest")]
    public decimal? QuoteInterest { get; set; }

    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; }

    [JsonProperty("availPos")]
    public decimal? AvailablePositions { get; set; }

    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }
    
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }
    
    [JsonProperty("uplLastPx")]
    public decimal? UnrealizedProfitAndLossLastPrice { get; set; }

    [JsonProperty("uplRatioLastPx")]
    public decimal? UnrealizedProfitAndLossRatioLastPrice { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    [JsonProperty("liabCcy")]
    public string LiabilitiesCurrency { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    [JsonProperty("optVal")]
    public decimal? OptionValue { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    [JsonProperty("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }
    
    [JsonProperty("usdPx")]
    public decimal? UsdPrice { get; set; }

    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }

    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }

    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }

    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }

    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }

    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }

    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }
    
    [JsonProperty("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }
    
    [JsonProperty("spotInUseCcy")]
    public string SpotInUseCurrency { get; set; }
    
    [JsonProperty("bizRefId")]
    public string ExternalBusinessId { get; set; }
    
    [JsonProperty("bizRefType")]
    public string ExternalBusinessType { get; set; }

    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("pTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime pTime { get; set; }

    [JsonProperty("closeOrderAlgo")]
    public IEnumerable<OkxCloseAlgoOrder> CloseAlgoOrders { get; set; }
}

public class OkxCloseAlgoOrder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoId { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("slTriggerPxType"), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType"), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonProperty("closeFraction")]
    public decimal? CloseFraction { get; set; }
}
