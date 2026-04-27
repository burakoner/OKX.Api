namespace OKX.Api.Account;

/// <summary>
/// Precheck result for switching delta neutral strategy
/// </summary>
public record OkxAccountPrecheckSetDeltaNeutral
{
    /// <summary>
    /// Unmatched information list
    /// </summary>
    [JsonProperty("unmatchedInfoCheck")]
    public List<OkxAccountPrecheckModeUnmatchedInformation>? UnmatchedInformationList { get; set; } = [];
}
