namespace OKX.Api.Models.TradingAccount;

public class OkxPositionHistory
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    /// <summary>
    /// The type of closing position
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(ClosingPositionTypeConverter))]
    public OkxClosingPositionType Type { get; set; }

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
    /// Average price of opening position
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal? CloseAveragePrice { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Max quantity of position
    /// </summary>
    [JsonProperty("openMaxPos")]
    public decimal? OpenMaxPos { get; set; }

    /// <summary>
    /// Position's cumulative closed volume
    /// </summary>
    [JsonProperty("closeTotalPos")]
    public decimal? CloseTotalPos { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }

    /// <summary>
    /// P&L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal? ProfitLossRatio { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Direction: long short
    /// Only applicable to MARGIN/FUTURES/SWAP/OPTION
        /// </summary>
        [JsonProperty("direction"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide Direction { get; set; }

    /// <summary>
    /// trigger mark price
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// Estimated liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("uly")]
    public string Currency { get; set; }
}