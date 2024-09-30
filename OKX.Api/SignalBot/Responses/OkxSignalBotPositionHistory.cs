namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Order Position History
/// </summary>
public class OkxSignalBotPositionHistory
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode MarginMode { get; set; }
    
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
    public long UpdateTimestamp { get; set; }
    
    /// <summary>
    /// Algo order updated time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Average price of opening position
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal AverageOpenPrice { get; set; }

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal AverageClosePrice { get; set; }
    
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
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }
    
    /// <summary>
    /// Direction: long short
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(OkxTradePositionDirectionConverter))]
    public OkxTradePositionDirection Direction { get; set; }
    
    /// <summary>
    /// Underlying { get; set; }
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;
}