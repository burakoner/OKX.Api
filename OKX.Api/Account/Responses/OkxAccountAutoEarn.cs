namespace OKX.Api.Account;

/// <summary>
/// OKX Account Auto Earn
/// </summary>
public record OkxAccountAutoEarn
{
    /// <summary>
    /// Auto earn type
    /// </summary>
    [JsonProperty("earnType")]
    public OkxAccountAutoEarnType Type { get; set; }

    /// <summary>
    /// Auto earn operation action
    /// </summary>
    [JsonProperty("action")]
    public OkxAccountAutoEarnAction Action { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}