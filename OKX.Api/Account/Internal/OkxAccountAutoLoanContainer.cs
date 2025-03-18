namespace OKX.Api.Account;

/// <summary>
/// Okx Auto Loan
/// </summary>
internal record OkxAccountAutoLoanContainer
{
    /// <summary>
    /// Auto Loan
    /// </summary>
    [JsonProperty("autoLoan")]
    public bool Payload { get; set; }
}