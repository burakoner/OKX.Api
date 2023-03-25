namespace OKX.Api.Models.SubAccount;

public class OkxSubAccount
{
    [JsonProperty("enable")]
    public bool Enable { get; set; }

    [JsonProperty("gAuth")]
    public bool GoogleAuth { get; set; }

    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("canTransOut")]
    public bool CanTransOut { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(SubAccountTypeConverter))]
    public OkxSubAccountType Type { get; set; }
}
