namespace OKX.Api.Block;

/// <summary>
/// OKX Block Account Level Allocation Request
/// </summary>
public record OkxBlockAllocationRequest
{
    /// <summary>
    /// The name of the allocated account of the RFQ.
    /// </summary>
    [JsonProperty("acct")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// The allocated legs of the account.
    /// </summary>
    [JsonProperty("legs")]
    public IEnumerable<OkxBlockAllocationLegRequest> Legs { get; set; } = [];
}

/// <summary>
/// OKX Block Account Level Allocation Leg Request
/// </summary>
public record OkxBlockAllocationLegRequest
{
    /// <summary>
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// The Instrument ID of each leg. Example : "BTC-USDT-SWAP"
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Margin currency.
    /// </summary>
    [JsonProperty("ccy", NullValueHandling = NullValueHandling.Ignore)]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradePositionSide? PositionSide { get; set; }
}