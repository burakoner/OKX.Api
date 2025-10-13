namespace OKX.Api.Account;

/// <summary>
/// Okx Simulated MMR Request
/// </summary>
public record OkxAccountSimulatedMmrRequest
{
    /// <summary>
    /// Switch to account mode
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("acctLv", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAccountMode? AccountLevel { get; set; }

    /// <summary>
    /// Cross margin leverage in Multi-currency margin mode, the default is 1.
    /// If the allowed leverage is exceeded, set according to the maximum leverage.
    /// Only applicable to Multi-currency margin
    /// </summary>
    [JsonProperty("lever")]
    public string Leverage { get; set; } = string.Empty;
}