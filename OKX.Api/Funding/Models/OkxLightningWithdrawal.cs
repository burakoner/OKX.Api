namespace OKX.Api.Funding.Models;

public class OkxLightningWithdrawal
{
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}