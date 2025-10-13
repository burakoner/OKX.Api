namespace OKX.Api.Account;

/// <summary>
/// Okx Move Position Request
/// </summary>
public record OkxAccountMovePositionRequest
{
    /// <summary>
    /// Source account name. If it's a master account, it should be "0"
    /// </summary>
    [JsonProperty("fromAcct")]
    public string FromAccount { get; set; } = string.Empty;

    /// <summary>
    /// Destination account name. If it's a master account, it should be "0"
    /// </summary>
    [JsonProperty("toAcct")]
    public string ToAccount { get; set; } = string.Empty;

    /// <summary>
    /// An array of objects containing details of each position to be moved
    /// </summary>
    [JsonProperty("legs")]
    public IEnumerable<OkxAccountMovePositionLegRequest> Legs { get; set; } = [];

    /// <summary>
    /// Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;
}

/// <summary>
/// Okx Move Position Leg Request
/// </summary>
public record OkxAccountMovePositionLegRequest
{
    /// <summary>
    /// Details of the position in the source account
    /// </summary>
    [JsonProperty("from")]
    public OkxAccountMovePositionLegFromRequest From { get; set; } = default!;

    /// <summary>
    /// Details of the configuration of the destination account
    /// </summary>
    [JsonProperty("to")]
    public OkxAccountMovePositionLegToRequest To { get; set; } = default!;
}

/// <summary>
/// Okx Move Position Leg From Request
/// </summary>
public record OkxAccountMovePositionLegFromRequest
{
    /// <summary>
    /// Position ID in the source account
    /// </summary>
    [JsonProperty("posId")]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// Number of contracts.
    /// </summary>
    [JsonProperty("sz")]
    public string Quantity { get; set; } = string.Empty;

    /// <summary>
    /// Trade side from the perspective of source account
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }
}

/// <summary>
/// Okx Move Position Leg To Request
/// </summary>
public record OkxAccountMovePositionLegToRequest
{
    /// <summary>
    /// Trading mode in the destination account.
    /// If not provided, tdMode will take the default values as shown below:
    /// Buy options in Futures mode/Multi-currency margin mode: isolated
    /// Other cases: cross
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("tdMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Position side
    /// This parameter is not mandatory if the destination sub-account is in net mode.If you pass it through, the only valid value is net.It can only be long or short if the destination sub-account is in long/short mode. If not specified, destination account in long/short mode always open new positions.
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("posSide", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Margin currency in destination accountOnly applicable to cross margin positions in Futures mode.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}