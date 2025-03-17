namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Order
/// </summary>
public record OkxSignalBot
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
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Instrument IDs
    /// </summary>
    [JsonProperty("instIds")]
    public List<string> InstrumentIds { get; set; } = [];
    
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
    /// Algo order type. contract: Contract signal
    /// </summary>
    [JsonProperty("algoOrdType")]
    public string AlgoOrderType { get; set; } = string.Empty;

    /// <summary>
    /// Algo order state
    /// starting
    /// running
    /// stopping
    /// </summary>
    [JsonProperty("state")]
    public OkxSignalBotOrderState State { get; set; }
    
    /// <summary>
    /// Algo order stop reason
    /// </summary>
    [JsonProperty("cancelType")]
    public string CancelType { get; set; } = string.Empty;
    
    /// <summary>
    /// Total P&amp;L
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalPnl { get; set; }
    
    /// <summary>
    /// Total P&amp;L ratio
    /// </summary>
    [JsonProperty("totalPnlRatio")]
    public decimal? TotalPnlRatio { get; set; }
    
    /// <summary>
    /// Total equity of strategy account
    /// </summary>
    [JsonProperty("totalEq")]
    public decimal? TotalEquity { get; set; }
    
    /// <summary>
    /// Float P&amp;L
    /// </summary>
    [JsonProperty("floatPnl")]
    public decimal? FloatPnl { get; set; }
    
    /// <summary>
    /// Realized P&amp;L
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal? RealizedPnl { get; set; }
    
    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal? FrozenBalance { get; set; }
    
    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("availBal")]
    public decimal? AvailableBalance { get; set; }
    
    /// <summary>
    /// Leverage
    /// Only applicable to contract signal
    /// </summary>
    [JsonProperty("lever")]
    public int? Leverage { get; set; }
    
    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("investAmt")]
    public decimal? InvestmentAmount { get; set; }
    
    /// <summary>
    /// Sub order type
    /// 1：limit order
    /// 2：market order
    /// 9：tradingView signal
    /// </summary>
    [JsonProperty("subOrdType")]
    public OkxSignalBotOrderType OrderType { get; set; }
    
    /// <summary>
    /// Price offset ratio, calculate the limit price as a percentage offset from the best bid/ask price
    /// Only applicable to subOrdType is limit order
    /// </summary>
    [JsonProperty("ratio")]
    public decimal? Ratio { get; set; }

    /// <summary>
    /// Entry setting parameters
    /// </summary>
    [JsonProperty("entrySettingParam")]
    public OkxSignalBotEntryParamaters? EntryParamaters { get; set; }

    /// <summary>
    /// Exit setting parameters
    /// </summary>
    [JsonProperty("exitSettingParam")]
    public OkxSignalBotExitParamaters? ExitParamaters { get; set; }
    
    /// <summary>
    /// Signal channel Id
    /// </summary>
    [JsonProperty("signalChanId")]
    public string SignalChannelId { get; set; } = string.Empty;
    
    /// <summary>
    /// Signal channel name
    /// </summary>
    [JsonProperty("signalChanName")]
    public string SignalChannelName { get; set; } = string.Empty;
    
    /// <summary>
    /// Signal source type
    /// 1: Created by yourself
    /// 2: Subscribe
    /// 3: Free signal
    /// </summary>
    [JsonProperty("signalSourceType")]
    public OkxSignalBotSourceType SignalSourceType { get; set; }
}