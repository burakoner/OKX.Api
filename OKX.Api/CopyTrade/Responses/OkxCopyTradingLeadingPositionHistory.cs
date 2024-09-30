namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxLeadingPositionHistory
/// </summary>
public class OkxCopyTradingLeadingPositionHistory
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";

    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Order ID for opening position
    /// </summary>
    [JsonProperty("openOrdId")]
    public long OpenOrderId { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Time of opening
    /// </summary>
    [JsonProperty("openTime")]
    public long OpenTimestamp { get; set; }

    /// <summary>
    /// Time of opening
    /// </summary>
    [JsonIgnore]
    public DateTime OpenTime => OpenTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("subPos")]
    public int QuantityOfPositions { get; set; }

    /// <summary>
    /// Time of closing position
    /// </summary>
    [JsonProperty("closeTime")]
    public long? CloseTimestamp { get; set; }

    /// <summary>
    /// Time of closing position
    /// </summary>
    [JsonIgnore]
    public DateTime? CloseTime => CloseTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal? AverageClosePrice { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// P&amp;L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal PnlRatio { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin
    /// </summary>
    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Latest mark price, only applicable to contract
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = "";

    /// <summary>
    /// Profit sharing amount, only applicable to copy trading
    /// </summary>
    [JsonProperty("profitSharingAmt")]
    public decimal? ProfitSharingAmount { get; set; }

    /// <summary>
    /// Quantity of positions that is already closed
    /// </summary>
    [JsonProperty("closeSubPos")]
    public int ClosedPositionCount { get; set; }

    /// <summary>
    /// The type of closing position
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxCopyTradingTypeOfClosingPositionConverter))]
    public OkxCopyTradingTypeOfClosingPosition? TypeOfClosingPosition { get; set; }
}
