namespace OKX.Api.Public;

/// <summary>
/// Event contract market.
/// </summary>
public record OkxPublicEventContractMarket
{
    /// <summary>
    /// Series ID.
    /// </summary>
    [JsonProperty("seriesId")]
    public string SeriesId { get; set; } = string.Empty;

    /// <summary>
    /// Event ID.
    /// </summary>
    [JsonProperty("eventId")]
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID.
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Listing timestamp.
    /// </summary>
    [JsonProperty("listTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? ListingTimestamp { get; set; }

    /// <summary>
    /// Listing time.
    /// </summary>
    [JsonIgnore]
    public DateTime? ListingTime => ListingTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Fixing timestamp, if applicable.
    /// </summary>
    [JsonProperty("fixTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? FixingTimestamp { get; set; }

    /// <summary>
    /// Fixing time, if applicable.
    /// </summary>
    [JsonIgnore]
    public DateTime? FixingTime => FixingTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Event expiry timestamp.
    /// </summary>
    [JsonProperty("expTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Event expiry time.
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime => ExpiryTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Market state.
    /// </summary>
    [JsonProperty("state")]
    public OkxInstrumentState State { get; set; }

    /// <summary>
    /// Whether the market has been disputed.
    /// </summary>
    [JsonProperty("disputed")]
    public bool IsDisputed { get; set; }

    /// <summary>
    /// Market outcome.
    /// </summary>
    [JsonProperty("outcome")]
    public OkxPublicEventContractMarketOutcome? Outcome { get; set; }

    /// <summary>
    /// Minimum expiration value that leads to YES.
    /// </summary>
    [JsonProperty("floorStrike")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FloorStrike { get; set; }

    /// <summary>
    /// Settlement value.
    /// </summary>
    [JsonProperty("settleValue")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SettlementValue { get; set; }
}
