namespace OKX.Api.Account;

/// <summary>
/// Account Preset Mode
/// </summary>
public record OkxAccountPresetMode
{
    /// <summary>
    /// Current account mode
    /// </summary>
    [JsonProperty("curAcctLv")]
    public OkxAccountMode CurrentAccountMode { get; set; }

    /// <summary>
    /// Account mode after switch
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountMode AccountModeAfterSwitch { get; set; }

    /// <summary>
    /// The leverage user preset for cross-margin positions
    /// </summary>
    [JsonProperty("lever")]
    public int? Leverage { get; set; }
}
