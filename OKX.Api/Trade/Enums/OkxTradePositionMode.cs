namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Mode
/// </summary>
public enum OkxTradePositionMode : byte
{
    /// <summary>
    /// LongShortMode
    /// </summary>
    [Map("long_short_mode")]
    LongShortMode = 1,

    /// <summary>
    /// NetMode
    /// </summary>
    [Map("net_mode")]
    NetMode = 2,
}