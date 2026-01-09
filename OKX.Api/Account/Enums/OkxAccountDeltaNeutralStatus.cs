namespace OKX.Api.Account;

/// <summary>
/// Okx Account Delta Neutral Status
/// </summary>
public enum OkxAccountDeltaNeutralStatus : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("0")]
    Normal = 0,

    /// <summary>
    /// Transfer restricted
    /// </summary>
    [Map("1")]
    TransferRestricted = 1,

    /// <summary>
    /// delta reducing - cancel all pending orders if delta is greater than 5000 USD, only one delta reducing order allowed per index (spot, futures, swap)
    /// </summary>
    [Map("2")]
    DeltaReducing = 2,
}