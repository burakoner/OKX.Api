namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Mode
/// </summary>
public enum OkxTradePositionMode
{
    /// <summary>
    /// LongShortMode
    /// </summary>
    [Map("long_short_mode")]
    LongShortMode,

    /// <summary>
    /// NetMode
    /// </summary>
    [Map("net_mode")]
    NetMode,
}