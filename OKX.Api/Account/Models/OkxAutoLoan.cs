namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Auto Loan
/// </summary>
public class OkxAutoLoan
{
    /// <summary>
    /// Auto Loan
    /// </summary>
    [JsonProperty("autoLoan")]
    public bool AutoLoan { get; set; }
}