namespace OKX.Api.Models.TradingAccount;

public class OkxAccountPositionMode
{
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }
}
