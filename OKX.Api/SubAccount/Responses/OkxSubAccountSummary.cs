namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Summary
/// </summary>
public record OkxSubAccountSummary
{
    /// <summary>
    /// Sub-account note
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Sub-account name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Sub-account uid
    /// </summary>
    [JsonProperty("uid")]
    public long SubAccountId { get; set; }

    /// <summary>
    /// Sub-account creation time, Unix timestamp in millisecond format. e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Sub-account creation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
