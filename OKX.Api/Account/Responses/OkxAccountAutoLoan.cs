namespace OKX.Api.Account;

/// <summary>
/// Okx Auto Loan
/// </summary>
public record OkxAccountAutoLoan
{
    /// <summary>
    /// Auto Loan
    /// </summary>
    [JsonProperty("autoLoan")]
    public bool AutoLoan { get; set; }
}