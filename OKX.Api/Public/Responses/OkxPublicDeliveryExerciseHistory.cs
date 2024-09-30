namespace OKX.Api.Public;

/// <summary>
/// OKX Delivery Exercise History
/// </summary>
public class OkxPublicDeliveryExerciseHistory
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicDeliveryExerciseHistoryData> Details { get; set; } = [];
}

/// <summary>
/// OKX Delivery Exercise History Detail
/// </summary>
public class OkxPublicDeliveryExerciseHistoryData
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("insId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxPublicDeliveryExerciseStatus Type { get; set; }
}
