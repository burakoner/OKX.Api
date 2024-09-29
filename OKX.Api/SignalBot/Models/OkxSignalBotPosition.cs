namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Order Position
/// </summary>
public class OkxSignalBotPosition
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = "";

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
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }
    
    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
    
    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AverageOpenPrice { get; set; }
    
    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
    
    /// <summary>
    /// Estimated liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }
    
    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }
    
    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("pos")]
    public decimal? QuantityOfPositions { get; set; }
    
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }
    
    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Initial margin requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMR { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MMR { get; set; }

    /// <summary>
    /// Unrealized profit and loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UPL { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio
    /// </summary>
    [JsonProperty("uplRatio")]
    public decimal? UplRatio { get; set; }

    /// <summary>
    /// Latest traded price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Notional value of positions in USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Auto decrease line, signal area
    /// Divided into 5 levels, from 1 to 5, the smaller the number, the weaker the adl intensity.
    /// </summary>
    [JsonProperty("adl")]
    public decimal? ADL { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }
}