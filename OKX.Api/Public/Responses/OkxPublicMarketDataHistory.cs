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
    /// Response timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Total size of all data files in MB
    /// </summary>
    [JsonProperty("totalSizeMB")]
    public decimal? TotalSizeMB { get; set; }

    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("dateAggrType")]
    public OkxPublicDateAggregationType DateAggregationType { get; set; }

    /// <summary>
    /// Announcement url
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
    /// Data range start date, Unix timestamp format in milliseconds (inclusive)
    /// </summary>
    [JsonProperty("dateRangeStart")]
    public long DateRangeStartTimestamp { get; set; }

    /// <summary>
    /// Data range start date, Unix timestamp format in milliseconds (inclusive)
    /// </summary>
    [JsonIgnore]
    public DateTime DateRangeStartTime => DateRangeStartTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Data range end date, Unix timestamp format in milliseconds (exclusive)
    /// </summary>
    [JsonProperty("dateRangeEnd")]
    public long DateRangeEndTimestamp { get; set; }

    /// <summary>
    /// Data range end date, Unix timestamp format in milliseconds (exclusive)
    /// </summary>
    [JsonIgnore]
    public DateTime DateRangeEndTime => DateRangeEndTimestamp.ConvertFromMilliseconds();

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

    /// <summary>
    /// Data date timestamp, Unix timestamp format in milliseconds
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

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
