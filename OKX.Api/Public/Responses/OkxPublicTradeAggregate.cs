namespace OKX.Api.Public;

/// <summary>
/// OKX Trade All
/// </summary>
public record OkxPublicTradeAggregate : OkxPublicTrade
{
    /// <summary>
    /// Count
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// Sequence ID
    /// </summary>
    [JsonProperty("seqId")]
    public long SequenceId { get; set; }
}
