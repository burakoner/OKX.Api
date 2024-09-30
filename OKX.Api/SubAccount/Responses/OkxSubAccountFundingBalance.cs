namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Funding Balance
/// </summary>
public record OkxSubAccountFundingBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Frozen Balance
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal FrozenBalance { get; set; }

    /// <summary>
    /// Available Balance
    /// </summary>
    [JsonProperty("availBal")]
    public decimal AvailableBalance { get; set; }
}
