namespace OKX.Api.Models.Trade;

public class OkxClosePositionResponse
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }
}
