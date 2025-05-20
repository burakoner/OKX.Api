namespace OKX.Api.Account;

/// <summary>
/// OkxAccountPositionBalanceUpdate
/// </summary>
public record OkxAccountPositionBalanceUpdate
{
    /// <summary>
    /// Update time of account information, millisecond format of Unix timestamp, e.g. 1597026383085
    /// </summary>
    [JsonProperty("pTime")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time of account information
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Event Type
    /// </summary>
    [JsonProperty("eventType")]
    public OkxAccountPositionBalanceUpdateEventType EventType { get; set; }

    /// <summary>
    /// Detailed asset information in all currencies
    /// </summary>
    [JsonProperty("balData")]
    public List<OkxAccountPositionBalanceUpdateBalance> Balances { get; set; } = [];

    /// <summary>
    /// Detailed position information in all currencies
    /// </summary>
    [JsonProperty("posData")]
    public List<OkxAccountPositionBalanceUpdatePosition> Positions { get; set; } = [];

    /// <summary>
    /// Details of trade
    /// </summary>
    [JsonProperty("trades")]
    public List<OkxAccountPositionBalanceUpdateTrade> Trades { get; set; } = [];
}

/// <summary>
/// OkxAccountPositionBalanceUpdateData
/// </summary>
public record OkxAccountPositionBalanceUpdateBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Equity of the currency
    /// </summary>
    [JsonProperty("cashBal")]
    public decimal CashBalance { get; set; }

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OkxAccountPositionBalanceUpdatePosition
/// </summary>
public record OkxAccountPositionBalanceUpdatePosition
{
    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-180216
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Quantity of positions contract. In the mode of autonomous transfer from position to position, after the deposit is transferred, a position with pos of 0 will be generated
    /// </summary>
    [JsonProperty("pos")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Position currency, only applicable to MARGIN positions.
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Non-Settlement entry price
    /// The non-settlement entry price only reflects the average price at which the position is opened or increased.
    /// Applicable to FUTURES cross
    /// </summary>
    [JsonProperty("nonSettleAvgPx")]
    public decimal? NonSettleAveragePrice { get; set; }

    /// <summary>
    /// Accumulated settled P&amp;L (calculated by settlement price)
    /// Applicable to FUTURES cross
    /// </summary>
    [JsonProperty("settledPnl")]
    public decimal? AccumulatedSettledPnl { get; set; }

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OkxAccountPositionBalanceUpdateTrade
/// </summary>
public record OkxAccountPositionBalanceUpdateTrade
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USD-180216
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }
}
