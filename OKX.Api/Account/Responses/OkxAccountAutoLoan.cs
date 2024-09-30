namespace OKX.Api.Account;

/// <summary>
/// Okx Auto Loan
/// </summary>
internal record OkxAccountAutoLoan
{
    /// <summary>
    /// Auto Loan
    /// </summary>
    [JsonProperty("autoLoan")]
    public bool Data { get; set; }
}