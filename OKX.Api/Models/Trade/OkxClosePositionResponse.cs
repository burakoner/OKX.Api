using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Models.Trade;

public class OkxClosePositionResponse
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }
}
