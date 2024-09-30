namespace OKX.Api.Public;

/// <summary>
/// OKX Open Interest
/// </summary>
public class OkxPublicOpenInterest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Open Interest Cont
    /// </summary>
    [JsonProperty("oi")]
    public long OpenInterestCount { get; set; }
    
    /// <summary>
    /// Open Interest Coin
    /// </summary>
    [JsonProperty("oiCcy")]
    public decimal OpenInterestCoin { get; set; }

    /// <summary>
    /// Open Interest Coin
    /// </summary>
    [JsonProperty("oiUsd")]
    public decimal OpenInterestUsd { get; set; }

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
}
