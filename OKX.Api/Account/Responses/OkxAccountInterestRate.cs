namespace OKX.Api.Account;

/// <summary>
/// OkxInterestRate
/// </summary>
public record OkxAccountInterestRate
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
    public string Currency { get; set; } = string.Empty;
}
