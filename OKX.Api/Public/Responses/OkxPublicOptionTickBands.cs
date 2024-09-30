namespace OKX.Api.Public;

/// <summary>
/// Option Tick Bands
/// </summary>
public class OkxPublicOptionTickBands
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Tick size band
    /// </summary>
    [JsonProperty("tickBand")]
    public List<OkxPublicOptionTickBand> TickBands { get; set; } = [];

}

/// <summary>
/// Option Tick Band
/// </summary>
public class OkxPublicOptionTickBand
{
    /// <summary>
    /// Minimum price while placing an order
    /// </summary>
    [JsonProperty("minPx")]
    public decimal MinimumPrice { get; set; }

    /// <summary>
    /// Maximum price while placing an order
    /// </summary>
    [JsonProperty("maxPx")]
    public decimal MaximumPrice { get; set; }

    /// <summary>
    /// Tick size, e.g. 0.0001
    /// </summary>
    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }
}