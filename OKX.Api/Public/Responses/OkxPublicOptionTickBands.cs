namespace OKX.Api.Public;

/// <summary>
/// Instrument tick bands for OPTION or EVENTS instruments.
/// </summary>
public record OkxPublicOptionTickBands
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Instrument family. Only applicable to OPTION.
    /// </summary>
    [JsonProperty("instFamily")]
    public string? InstrumentFamily { get; set; }

    /// <summary>
    /// Tick size bands. EVENTS returns unified bands for all event contracts.
    /// </summary>
    [JsonProperty("tickBand")]
    public List<OkxPublicOptionTickBand> TickBands { get; set; } = [];

}

/// <summary>
/// Option Tick Band
/// </summary>
public record OkxPublicOptionTickBand
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
