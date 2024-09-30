namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxCopyTradingCopySettings
/// </summary>
public class OkxCopyTradingCopySettings
{
    /// <summary>
    /// Copy mode
    /// </summary>
    [JsonProperty("copyMode"), JsonConverter(typeof(OkxCopyTradingModeConverter))]
    public OkxCopyTradingMode CopyMode { get; set; }

    /// <summary>
    /// Copy amount in USDT per order.
    /// </summary>
    [JsonProperty("copyAmt")]
    public decimal? CopyAmount { get; set; }

    /// <summary>
    /// Copy ratio per order.
    /// </summary>
    [JsonProperty("copyRatio")]
    public decimal? CopyRatio { get; set; }

    /// <summary>
    /// Maximum total amount in USDT.
    /// The maximum total amount you'll invest at any given time across all orders in this copy trade
    /// </summary>
    [JsonProperty("copyTotalAmt")]
    public decimal? CopyTotalAmount { get; set; }

    /// <summary>
    /// Take profit per order. 0.1 represents 10%
    /// </summary>
    [JsonProperty("tpRatio")]
    public decimal? TakeProfitRatio { get; set; }

    /// <summary>
    /// Stop loss per order. 0.1 represents 10%
    /// </summary>
    [JsonProperty("slRatio")]
    public decimal? StopLossRatio { get; set; }

    /// <summary>
    /// Copy contract type setted
    /// </summary>
    [JsonProperty("copyInstIdType"), JsonConverter(typeof(OkxCopyTradingInstrumentIdTypeConverter))]
    public OkxCopyTradingInstrumentIdType CopyInstrumentIdType { get; set; }

    /// <summary>
    /// Instrument list. It will return all lead contracts of the lead trader
    /// </summary>
    [JsonProperty("instIds")]
    public List<OkxCopyTradingCopyInstrumentSettings> Instruments { get; set; } = [];

    /// <summary>
    /// Total stop loss in USDT for trader.
    /// </summary>
    [JsonProperty("slTotalAmt")]
    public decimal? StopLossAmount { get; set; }

    /// <summary>
    /// Action type for open positions
    /// </summary>
    [JsonProperty("subPosCloseType"), JsonConverter(typeof(OkxCopyTradingPositionCloseTypeConverter))]
    public OkxCopyTradingPositionCloseType PositionCloseType { get; set; }

    /// <summary>
    /// Copy margin mode
    /// </summary>
    [JsonProperty("copyMgnMode"), JsonConverter(typeof(OkxCopyTradingMarginModeConverter))]
    public OkxCopyTradingMarginMode MarginMode { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Current copy state
    /// </summary>
    [JsonProperty("copyState"), JsonConverter(typeof(OkxCopyTradingStateConverter))]
    public OkxCopyTradingState State { get; set; }
}

/// <summary>
/// OKX Copy Trading Copy Instrument Settings
/// </summary>
public class OkxCopyTradingCopyInstrumentSettings
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Whether copying this instId
    /// </summary>
    [JsonProperty("enabled"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Enabled { get; set; }
}