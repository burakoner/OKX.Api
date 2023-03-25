namespace OKX.Api.Models.Funding;

public class OkxLightningDeposit
{
    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("invoice")]
    public string Invoice { get; set; }
}