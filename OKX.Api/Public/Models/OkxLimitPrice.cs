namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Limit Price
/// </summary>
public class OkxLimitPrice
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
    public string InstrumentId { get; set; }

    /// <summary>
    /// Buy Limit
    /// </summary>
    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    /// <summary>
    /// Sell Limit
    /// </summary>
    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Whether price limit is effective
    /// true: the price limit is effective
    /// false: the price limit is not effective
    /// </summary>
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

}
