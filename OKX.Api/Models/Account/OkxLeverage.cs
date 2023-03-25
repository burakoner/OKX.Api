namespace OKX.Api.Models.Account;

public class OkxLeverage
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}
