namespace OKX.Api.Models.FundingAccount;

public class OkxLightningDeposit
{
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("invoice")]
    public string Invoice { get; set; }
}