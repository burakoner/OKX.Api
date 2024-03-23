namespace OKX.Api.SubAccount.Models;

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

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
