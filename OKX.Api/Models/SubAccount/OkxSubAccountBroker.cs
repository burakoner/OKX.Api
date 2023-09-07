namespace OKX.Api.Models.SubAccount;

public class OkxSubAccountBroker
{
    [JsonProperty("type"), JsonConverter(typeof(SubAccountTypeConverter))]
    public OkxSubAccountType Type { get; set; }


    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("uid")]
    public string uid { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
