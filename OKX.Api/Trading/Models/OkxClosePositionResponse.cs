using OKX.Api.Trading.Converters;
using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Models;

/// <summary>
/// OKX Close Position Response
/// </summary>
public class OkxClosePositionResponse
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Position Side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }
}
