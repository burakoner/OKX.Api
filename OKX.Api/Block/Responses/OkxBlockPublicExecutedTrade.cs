namespace OKX.Api.Block;

/// <summary>
/// OKX Block Public Executed Trade
/// </summary>
public record OkxBlockPublicExecutedTrade
{
    /// <summary>
    /// Option strategy, e.g. CALL_CALENDAR_SPREAD
    /// </summary>
    [JsonProperty("strategy")]
    public string Strategy { get; set; } = string.Empty;

    /// <summary>
    /// The time the trade was executed. Unix timestamp in milliseconds.
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The time the trade was executed.
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Block trade ID.
    /// </summary>
    [JsonProperty("blockTdId"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? BlockTradeId { get; set; }

    /// <summary>
    /// Group RFQ ID. Only applicable to group RFQ, return "" for normal RFQ.
    /// </summary>
    [JsonProperty("groupId")]
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxBlockLegExecuted> Legs { get; set; } = [];
}
