namespace OKX.Api.Account;

/// <summary>
/// OKX Account Move Position
/// </summary>
public record OkxAccountMovePosition
{
    /// <summary>
    /// Block trade ID
    /// </summary>
    [JsonProperty("blockTdId")]
    public long BlockTradeId { get; set; }

    /// <summary>
    /// Client-supplied ID
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Status of the order filled, failed
    /// </summary>
    [JsonProperty("state")]
    public OkxAccountMovePositionState State { get; set; }

    /// <summary>
    /// Source account name
    /// </summary>
    [JsonProperty("fromAcct")]
    public string FromAccount { get; set; } = string.Empty;

    /// <summary>
    /// Destination account name
    /// </summary>
    [JsonProperty("toAcct")]
    public string ToAccount { get; set; } = string.Empty;

    /// <summary>
    /// An array of objects containing details of each position to be moved
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxAccountMovePositionLeg> Legs { get; set; } = [];

    /// <summary>
    /// Unix timestamp in milliseconds indicating when the transfer request was processed
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Unix timestamp in milliseconds indicating when the transfer request was processed
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OKX Account Move Position Leg
/// </summary>
public record OkxAccountMovePositionLeg
{
    /// <summary>
    /// Object describing the "from" leg
    /// </summary>
    [JsonProperty("from")]
    public OkxAccountMovePositionLegFrom From { get; set; } = default!;

    /// <summary>
    /// Object describing the "to" leg
    /// </summary>
    [JsonProperty("to")]
    public OkxAccountMovePositionLegTo To { get; set; } = default!;
}

/// <summary>
/// OKX Account Move Position Leg From
/// </summary>
public record OkxAccountMovePositionLegFrom : OkxRestApiErrorBase
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Transfer price, typically a 60-minute TWAP of the mark price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Direction of the leg in the source account
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide? Side { get; set; }

    /// <summary>
    /// Number of Contracts
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }
}

/// <summary>
/// OKX Account Move Position Leg To
/// </summary>
public record OkxAccountMovePositionLegTo : OkxRestApiErrorBase
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Transfer price, typically a 60-minute TWAP of the mark price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Trade side of the trade in the destination account
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide? Side { get; set; }

    /// <summary>
    /// Number of Contracts
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Position side of the trade in the destination account
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}
