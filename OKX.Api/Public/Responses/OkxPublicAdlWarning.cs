namespace OKX.Api.Public;

/// <summary>
/// OKX ADL Warning
/// </summary>
public record OkxPublicAdlWarning
{
    /// <summary>
    /// Maximum security fund balance in the past eight hours
    /// Deprecated by OKX and currently returned as an empty string
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("maxBal")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumBalance { get; set; }

    /// <summary>
    /// security fund balance that turns off ADL
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlRecBal")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AdlRecordBalance { get; set; }

    /// <summary>
    /// Real-time security fund balance
    /// </summary>
    [JsonProperty("bal")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [Obsolete("The ADL warning channel does not provide instId.")]
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when security fund balance reached maximum in the past eight hours, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("maxBalTs")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? MaximumBalanceTimestamp { get; set; }

    /// <summary>
    /// Timestamp when security fund balance reached maximum in the past eight hours
    /// </summary>
    [Obsolete("OKX has deprecated MaximumBalanceTimestamp; this convenience property is scheduled for removal with it.")]
    [JsonIgnore]
    public DateTime? MaximumBalanceTime => MaximumBalanceTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// ADL related events
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlType")]
    public OkxPublicAdlEvent? AdlType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxPublicAdlState? State { get; set; }

    /// <summary>
    /// security fund balance that triggers ADL
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlBal")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AdlBalance { get; set; }

    /// <summary>
    /// Data push time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Real-time security fund decline rate (deprecated)
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("decRate")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeclineRate { get; set; }

    /// <summary>
    /// Security fund decline rate that triggers ADL (deprecated)
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlRate")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AdlRate { get; set; }

    /// <summary>
    /// Security fund decline rate that turns off ADL (deprecated)
    /// </summary>
    [Obsolete("OKX has deprecated this field; it currently returns an empty string and is scheduled for removal.")]
    [JsonProperty("adlRecRate")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AdlRecoveryRate { get; set; }

    /// <summary>
    /// Data push time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
