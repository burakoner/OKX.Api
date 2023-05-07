namespace OKX.Api.Models.SubAccount;

public class OkxSubAccountApiKey
{
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("apiKey")]
    public string apiKey { get; set; }

    [JsonProperty("perm")]
    public string Permissions { get; set; }

    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
