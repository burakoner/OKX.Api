namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account
/// </summary>
public record OkxSubAccount
{
    /// <summary>
    /// Sub-account type
    /// </summary>
    [JsonProperty("type")]
    public OkxSubAccountType Type { get; set; }

    /// <summary>
    /// Sub-account status
    /// </summary>
    [JsonProperty("enable")]
    public bool Enable { get; set; }

    /// <summary>
    /// Sub-account name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Sub-account uid
    /// </summary>
    [JsonProperty("uid")]
    public string SubAccountId { get; set; } = string.Empty;

    /// <summary>
    /// Sub-account note
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Mobile number that linked with the sub-account.
    /// </summary>
    [JsonProperty("mobile")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// If the sub-account switches on the Google Authenticator for login authentication.
    /// true: On false: Off
    /// </summary>
    [JsonProperty("gAuth")]
    public bool GoogleAuth { get; set; }

    /// <summary>
    /// Frozen functions
    /// trading
    /// convert
    /// transfer
    /// withdrawal
    /// deposit
    /// flexible_loan
    /// </summary>
    [JsonProperty("frozenFunc")]
    public List<string> FrozenFunctions { get; set; }=[];

    /// <summary>
    /// Whether the sub-account has the right to transfer out.
    /// true: can transfer out
    /// false: cannot transfer out
    /// </summary>
    [JsonProperty("canTransOut")]
    public bool CanTransOut { get; set; }

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
