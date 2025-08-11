namespace OKX.Api.Account;

/// <summary>
/// OkxPositionHistory
/// </summary>
public record OkxAccountPositionHistory
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

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
    /// The type of closing position
    /// </summary>
    [JsonProperty("type")]
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
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Average price of opening position
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal OpenAveragePrice { get; set; }

    /// <summary>
    /// Non-settlement entry price
    /// The non-settlement entry price only reflects the average price at which the position is opened or increased.
    /// Applicable to cross FUTURES positions.
    /// </summary>
    [JsonProperty("nonSettleAvgPx")]
    public decimal? NonSettlementAveragePrice { get; set; }

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal CloseAveragePrice { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Max quantity of position
    /// </summary>
    [JsonProperty("openMaxPos")]
    public decimal OpenMaxPos { get; set; }

    /// <summary>
    /// Position's cumulative closed volume
    /// </summary>
    [JsonProperty("closeTotalPos")]
    public decimal CloseTotalPos { get; set; }

    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal RealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Settled profit and loss (calculated by settlement price)
    /// Only applicable to cross FUTURES
    /// </summary>
    [JsonProperty("settledPnl")]
    public decimal? SettledProfitAndLoss { get; set; }

    /// <summary>
    /// P&amp;L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }

    /// <summary>
    /// Accumulated fee
    /// Negative number represents the user transaction fee charged by the platform.Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Accumulated funding fee
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal FundingFee { get; set; }

    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonProperty("liqPenalty")]
    public decimal LiquidationPenalty { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal ProfitAndLoss { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Direction: long short
    /// Only applicable to MARGIN/FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("direction")]
    public OkxTradePositionDirection Direction { get; set; }

    /// <summary>
    /// trigger mark price
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}