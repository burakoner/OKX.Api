namespace OKX.Api.Models.FundingAccount;

public class OkxLightningDeposit
{
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("invoice")]
    public string Invoice { get; set; }
}