namespace OKX.Api.Public;

/// <summary>
/// Option Premium History
/// </summary>
public record OkxPublicPremiumHistory
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Premium between the mid price of perps market and the index price
    /// </summary>
    [JsonProperty("premium")]
    public decimal Premium { get; set; }

    /// <summary>
    /// Data generation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}