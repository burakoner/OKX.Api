namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Lead Trader Position
/// </summary>
public class OkxCopyTradingLeadTraderPositionHistory
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";

    /// <summary>
    /// Lead position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide Side { get; set; }

    /// <summary>
    /// Margin mode. cross isolated
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal AverageOpenPrice { get; set; }

    /// <summary>
    /// Open time
    /// </summary>
    [JsonProperty("openTime")]
    public long OpenTimestamp { get; set; }

    /// <summary>
    /// Open time
    /// </summary>
    [JsonIgnore]
    public DateTime OpenTime => OpenTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("subPos")]
    public int PositionCount { get; set; }
    
    /// <summary>
    /// Close time
    /// </summary>
    [JsonProperty("closeTime")]
    public long CloseTimestamp { get; set; }

    /// <summary>
    /// Close time
    /// </summary>
    [JsonIgnore]
    public DateTime CloseTime => CloseTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal AverageClosePrice { get; set; }
    
    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal Pnl { get; set; }

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
    public decimal Margin { get; set; }
    
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = "";
}
