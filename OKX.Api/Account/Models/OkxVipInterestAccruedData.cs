namespace OKX.Api.Account.Models;

/// <summary>
/// VIP Interest Accrued Data
/// </summary>
public class OkxVipInterestAccruedData
{
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    [JsonProperty("interestRate")]
    public decimal InterestRate { get; set; }

    [JsonProperty("liab")]
    public decimal Liability { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
