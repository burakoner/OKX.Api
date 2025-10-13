namespace OKX.Api.Account;

/// <summary>
/// OKX Account Settle Currency
/// </summary>
public record OkxAccountSettleCurrency
{
    /// <summary>
    /// USD-margined contract settle currency
    /// </summary>
    [JsonProperty("settleCcy")]
    public string SettleCurrency { get; set; } = string.Empty;
}