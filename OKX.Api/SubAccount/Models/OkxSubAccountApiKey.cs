namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub Account Api Key
/// </summary>
public class OkxSubAccountApiKey
{
    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }
    
    /// <summary>
    /// API Key
    /// </summary>
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; }

    /// <summary>
    /// Label
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonProperty("perm")]
    public string Permissions { get; set; }

    /// <summary>
    /// IP Addresses
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
