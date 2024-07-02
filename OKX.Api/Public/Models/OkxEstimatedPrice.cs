using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Estimated Price
/// </summary>
public class OkxEstimatedPrice
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Estimated Price
    /// </summary>
    [JsonProperty("settlePx")]
    public decimal EstimatedPrice { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
