namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Side
/// </summary>
public enum OkxTradePositionSide : int
{
    /// <summary>
    /// Long
    /// </summary>
    [Map("long")]
    Long = 1,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short = -1,

    /// <summary>
    /// Net
    /// </summary>
    [Map("net")]
    Net = 0,
}