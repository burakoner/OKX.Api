namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Public Executed Trade
/// </summary>
public class OkxBlockPublicExecutedTrade
{
    /// <summary>
    /// Block trade ID.
    /// </summary>
    [JsonProperty("blockTdId")]
    public long BlockTradeId { get; set; }

    /// <summary>
    /// The time the trade was executed. Unix timestamp in milliseconds.
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The time the trade was executed.
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxBlockLegExecuted> Legs { get; set; } = [];
}
