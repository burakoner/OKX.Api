namespace OKX.Api.Models.TradingAccount;

public class OkxAccountGreeksType
{
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }
}
