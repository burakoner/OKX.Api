using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// Option Tick Bands
/// </summary>
public class OkxOptionTickBands
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
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Tick size band
    /// </summary>
    [JsonProperty("tickBand")]
    public List<OkxOptionTickBand> TickBands { get; set; }

}

/// <summary>
/// Option Tick Band
/// </summary>
public class OkxOptionTickBand
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