namespace OKX.Api.Block;

/// <summary>
/// OKX Block Public Structure Trade Update
/// </summary>
public record OkxBlockPublicStructureTradeUpdate
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("cTime")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("blockTdId")]
    public long BlockTradeId { get; set; }

    /// <summary>
    /// Group ID
    /// </summary>
    [JsonProperty("groupId")]
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxBlockPublicStructureTradeLeg> Legs { get; set; } = [];
}

/// <summary>
/// OkxBlockPublicStructureTradeLeg
/// </summary>
public record OkxBlockPublicStructureTradeLeg
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// The price the leg executed
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size of the leg in contracts or spot.
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }

    /// <summary>
    /// The direction of the leg from the Takers perspective. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Last traded ID.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }
}
