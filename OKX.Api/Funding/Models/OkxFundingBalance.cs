namespace OKX.Api.Funding.Models;

public class OkxFundingBalance
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("availBal")]
    public decimal Available { get; set; }

    [JsonProperty("bal")]
    public decimal Balance { get; set; }

    [JsonProperty("frozenBal")]
    public decimal Frozen { get; set; }
}
