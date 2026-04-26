namespace OKX.Api.Trade;

/// <summary>
/// Event contract trading outcome side.
/// </summary>
public enum OkxTradeEventOutcome : byte
{
    /// <summary>
    /// YES.
    /// </summary>
    [Map("yes")]
    Yes = 1,

    /// <summary>
    /// NO.
    /// </summary>
    [Map("no")]
    No = 2,
}
