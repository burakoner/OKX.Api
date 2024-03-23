using OKX.Api.SubAccount.Converters;
using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub-Account
/// </summary>
public class OkxSubAccount
{
    /// <summary>
    /// Sub-account type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxSubAccountTypeConverter))]
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
    public string SubAccountName { get; set; }

    /// <summary>
    /// Sub-account uid
    /// </summary>
    [JsonProperty("uid")]
    public string SubAccountId { get; set; }

    /// <summary>
    /// Sub-account note
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// Mobile number that linked with the sub-account.
    /// </summary>
    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// If the sub-account switches on the Google Authenticator for login authentication.
    /// true: On false: Off
    /// </summary>
    [JsonProperty("gAuth")]
    public bool GoogleAuth { get; set; }

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
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
