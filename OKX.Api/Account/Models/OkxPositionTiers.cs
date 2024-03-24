using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxPosition
/// </summary>
public class OkxPositionTiers
{
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    [JsonProperty("maxSz")]
    public long MaximumPositions { get; set; }

    [JsonProperty("posType"), JsonConverter(typeof(OkxPositionTypeConverter))]
    public OkxPositionType PositionType { get; set; }
}