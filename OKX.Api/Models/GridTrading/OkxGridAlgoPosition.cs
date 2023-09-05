﻿namespace OKX.Api.Models.GridTrading;

public class OkxGridAlgoPosition
{
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }


    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
    
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode? MarginMode { get; set; }

    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsdValue { get; set; }

    [JsonProperty("adl")]
    public decimal? AutoDecreaseLine { get; set; }

    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }
}