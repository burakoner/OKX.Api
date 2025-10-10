namespace OKX.Api.Block;

/// <summary>
/// OKX Block Execution Update
/// </summary>
public record OkxBlockStructureTradeUpdate : OkxBlockTrade
{
    /// <summary>
    /// Whether the trade is filled successfully
    /// </summary>
    [JsonProperty("isSuccessful")]
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Error code for unsuccessful trades.
    /// It is "" for successful trade.
    /// </summary>
    [JsonProperty("errorCode")]
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public new List<OkxBlockLegTradeUpdate> Legs { get; set; } = [];

    /// <summary>
    /// Account level allocation of the RFQ
    /// </summary>
    [JsonProperty("acctAlloc")]
    public new List<OkxBlockStructureTradeUpdateAllocation> AccountLevelAllocations { get; set; } = [];
}

/// <summary>
/// OKX Block Account Level Allocation Response
/// </summary>
public record OkxBlockStructureTradeUpdateAllocation
{
    /// <summary>
    /// Block trade ID
    /// </summary>
    [JsonProperty("blockTdId")]
    public string BlockTradeId { get; set; } = string.Empty;

    /// <summary>
    /// Error code for unsuccessful trades.It is "0" for successful trade.
    /// </summary>
    [JsonProperty("errorCode")]
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// The name of the allocated account of the RFQ.
    /// </summary>
    [JsonProperty("acct")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// The allocated legs of the account.
    /// </summary>
    [JsonProperty("legs")]
    public IEnumerable<OkxBlockStructureTradeUpdateAllocationLeg> Legs { get; set; } = [];
}

/// <summary>
/// OKX Block Account Level Allocation Leg Response
/// </summary>
public record OkxBlockStructureTradeUpdateAllocationLeg
{
    /// <summary>
    /// The Instrument ID of each leg. Example : "BTC-USDT-SWAP"
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Size { get; set; }

    /// <summary>
    /// Last traded ID of each account leg
    /// </summary>
    [JsonProperty("tradeId")]
    public string TradeId { get; set; } = string.Empty;

    /// <summary>
    /// The fee of each account level leg
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Fee currency. To be read in conjunction with fee
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;
}
