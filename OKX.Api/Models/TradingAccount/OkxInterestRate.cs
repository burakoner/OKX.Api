namespace OKX.Api.Models.TradingAccount;

public class OkxInterestRate
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("interestRate")]
    public decimal? InterestRate { get; set; }
}
