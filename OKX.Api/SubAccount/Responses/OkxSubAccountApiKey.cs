namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Api Key
/// </summary>
public class OkxSubAccountApiKey
{
    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
    
    /// <summary>
    /// API Key
    /// </summary>
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Label
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonProperty("perm")]
    public string Permissions { get; set; } = string.Empty;

    /// <summary>
    /// IP Addresses
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddresses { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
