namespace OKX.Api.Models.Account;

public class OkxAccountGreeksType
{
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }
}
