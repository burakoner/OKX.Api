namespace OKX.Api.Models.Account;

public class OkxWithdrawalAmount
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawal { get; set; }
}
