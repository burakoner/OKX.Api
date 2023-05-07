namespace OKX.Api.Models.Funding;

public class OkxLightningWithdrawal
{
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }
}