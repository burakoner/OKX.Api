namespace OKX.Api.Models.Account;

public class OkxMarginAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide? PositionSide { get; set; }

    [JsonProperty("amt")]
    public decimal? amt { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(MarginAddReduceConverter))]
    public OkxMarginAddReduce? MarginAddReduce { get; set; }
}
