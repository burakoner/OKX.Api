namespace OKX.Api.Public;

/// <summary>
/// OKX Public Market Data History
/// </summary>
public record OkxPublicMarketDataHistory
{
    /// <summary>
    /// Response timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Response timestamp as a UTC instant.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Total size of all data files in MB
    /// </summary>
    [JsonProperty("totalSizeMB")]
    public decimal? TotalSizeMB { get; set; }

    /// <summary>
    /// Daily or monthly file aggregation.
    /// </summary>
    [JsonProperty("dateAggrType")]
    public OkxPublicDateAggregationType DateAggregationType { get; set; }

    /// <summary>
    /// Historical data file groups.
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicMarketDataHistoryItem> Details { get; set; } = [];
}

/// <summary>
/// OKX Public Market Data History Item
/// </summary>
public record OkxPublicMarketDataHistoryItem
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Data range start date, Unix timestamp format in milliseconds (inclusive)
    /// </summary>
    [JsonProperty("dateRangeStart")]
    public long DateRangeStartTimestamp { get; set; }

    /// <summary>
    /// Inclusive range-start timestamp as a UTC instant, not the module's calendar date.
    /// Use <see cref="GetDateRangeStartDate"/> for the module-specific date.
    /// </summary>
    [JsonIgnore]
    public DateTime DateRangeStartTime => DateRangeStartTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Inclusive range-start calendar date: UTC for modules 4/5/6, UTC+8 for modules 1/2/3/11.
    /// Returns midnight with <see cref="DateTimeKind.Unspecified"/>.
    /// </summary>
    /// <param name="module">The module used in the request; it is not included in the response.</param>
    /// <exception cref="ArgumentOutOfRangeException">The module or timestamp is unsupported.</exception>
    public DateTime GetDateRangeStartDate(OkxPublicMarketDataHistoryModule module)
        => OkxPublicMarketDataHistoryDates.GetDate(DateRangeStartTimestamp, module);

    /// <summary>
    /// Data range end date, Unix timestamp format in milliseconds (inclusive)
    /// </summary>
    [JsonProperty("dateRangeEnd")]
    public long DateRangeEndTimestamp { get; set; }

    /// <summary>
    /// Inclusive range-end timestamp as a UTC instant, not the module's calendar date.
    /// Use <see cref="GetDateRangeEndDate"/> for the module-specific date.
    /// </summary>
    [JsonIgnore]
    public DateTime DateRangeEndTime => DateRangeEndTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Inclusive range-end calendar date: UTC for modules 4/5/6, UTC+8 for modules 1/2/3/11.
    /// Returns midnight with <see cref="DateTimeKind.Unspecified"/>.
    /// </summary>
    /// <param name="module">The module used in the request; it is not included in the response.</param>
    /// <exception cref="ArgumentOutOfRangeException">The module or timestamp is unsupported.</exception>
    public DateTime GetDateRangeEndDate(OkxPublicMarketDataHistoryModule module)
        => OkxPublicMarketDataHistoryDates.GetDate(DateRangeEndTimestamp, module);

    /// <summary>
    /// Data group size in MB
    /// </summary>
    [JsonProperty("groupSizeMB")]
    public decimal? GroupSizeMB { get; set; }

    /// <summary>
    /// Group details
    /// </summary>
    [JsonProperty("groupDetails")]
    public List<OkxPublicMarketDataHistoryItemGroupDetail> groupDetails { get; set; } = [];
}

/// <summary>
/// OKX Public Market Data History Item Group Detail
/// </summary>
public record OkxPublicMarketDataHistoryItemGroupDetail
{
    /// <summary>
    /// Data file name, e.g. BTC-USDT-SWAP-trades-2025-05-15.zip
    /// </summary>
    [JsonProperty("filename")]
    public string Filename { get; set; } = string.Empty;

    /// <summary>
    /// Data date timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("dataTs")]
    public long Timestamp { get; set; }

    // The current response example uses `dateTs` while the parameter table still documents `dataTs`.
    [JsonProperty("dateTs")]
    private long? TimestampAlias
    {
        set
        {
            if (value.HasValue)
                Timestamp = value.Value;
        }
    }

    /// <summary>
    /// Data timestamp as a UTC instant, not the file's module-specific calendar date.
    /// Use <see cref="GetDate"/> for the file date.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// File calendar date: UTC for modules 4/5/6, UTC+8 for modules 1/2/3/11.
    /// Returns midnight with <see cref="DateTimeKind.Unspecified"/>.
    /// </summary>
    /// <param name="module">The module used in the request; it is not included in the response.</param>
    /// <exception cref="ArgumentOutOfRangeException">The module or timestamp is unsupported.</exception>
    public DateTime GetDate(OkxPublicMarketDataHistoryModule module)
        => OkxPublicMarketDataHistoryDates.GetDate(Timestamp, module);

    /// <summary>
    /// File size in MB
    /// </summary>
    [JsonProperty("sizeMB")]
    public decimal? SizeMB { get; set; }

    /// <summary>
    /// Download URL
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
}
