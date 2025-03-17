namespace OKX.Api.Account;

/// <summary>
/// Okx Auto Repay
/// </summary>
internal record OkxAccountAutoRepay
{
    /// <summary>
    /// Auto Repay
    /// </summary>
    [JsonProperty("autoLoan")]
    public bool Data { get; set; }
}