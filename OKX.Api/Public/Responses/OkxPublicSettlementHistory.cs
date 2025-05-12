namespace OKX.Api.Public;

/// <summary>
/// OKX Settlement History
/// </summary>
public record OkxPublicSettlementHistory
{
    /// <summary>
    /// Settlement time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Settlement info
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicSettlementHistoryDetail> Details { get; set; } = [];
}

/// <summary>
/// OKX Settlement History Detail
/// </summary>
public record OkxPublicSettlementHistoryDetail
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Settlement price
    /// </summary>
    [JsonProperty("settlePx")]
    public decimal SettlementPrice { get; set; }
}