namespace OKX.Api.Account;

/// <summary>
/// OkxInterestRate
/// </summary>
public class OkxAccountInterestRate
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
