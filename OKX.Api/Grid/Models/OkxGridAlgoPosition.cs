using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Algo Position
/// </summary>
public class OkxGridAlgoPosition
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
    /// Created time of position
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Created time of position
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Last price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Notional value
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsdValue { get; set; }

    /// <summary>
    /// Auto increase line
    /// </summary>
    [JsonProperty("adl")]
    public decimal? AutoDecreaseLine { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }
}