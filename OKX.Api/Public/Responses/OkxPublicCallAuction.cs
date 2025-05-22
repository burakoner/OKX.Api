namespace OKX.Api.Public;

/// <summary>
/// OKX Call Auction Details
/// </summary>
public record OkxPublicCallAuction
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";

    /// <summary>
    /// Equilibrium price
    /// </summary>
    [JsonProperty("eqPx")]
    public decimal EquilibriumPrice { get; set; }

    /// <summary>
    /// Matched size for both buy and sell. The unit is in base currency
    /// </summary>
    [JsonProperty("matchedSz")]
    public decimal MatchedSize { get; set; }

    /// <summary>
    /// Unmatched size
    /// </summary>
    [JsonProperty("unmatchedSz")]
    public decimal UnMatchedSize { get; set; }

    /// <summary>
    /// Call auction end time. Unix timestamp in milliseconds.
    /// </summary>
    [JsonProperty("auctionEndTime")]
    public long AuctionEndTimestamp { get; set; }

    /// <summary>
    /// Call auction end time.
    /// </summary>
    [JsonIgnore]
    public DateTime AuctionEndTime => AuctionEndTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Trading state of the symbol
    /// </summary>
    [JsonProperty("state")]
    public OkxPublicCallAuctionState State { get; set; }

    /// <summary>
    /// Data generation time. Unix timestamp in millieseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data generation time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
