using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Trading.Models;

/// <summary>
/// OKX Mass Cancel Request
/// </summary>
public class OkxMassCancelRequest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }
}