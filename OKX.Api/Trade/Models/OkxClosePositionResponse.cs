using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

public class OkxClosePositionResponse
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }
}
