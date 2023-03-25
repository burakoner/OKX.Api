namespace OKX.Api.Models.Account;

public class OkxAccountPositionMode
{
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }
}
