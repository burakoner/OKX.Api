namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxInterestRate
/// </summary>
public class OkxInterestRate
{
    /// <summary>
    /// interest rate
    /// </summary>
    [JsonProperty("interestRate")]
    public decimal InterestRate { get; set; }

    /// <summary>
    /// currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
}
