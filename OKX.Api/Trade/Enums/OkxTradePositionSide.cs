namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Side
/// </summary>
public enum OkxTradePositionSide
{
    /// <summary>
    /// Long
    /// </summary>
    [Map("long")]
    Long,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short,

    /// <summary>
    /// Net
    /// </summary>
    [Map("net")]
    Net,
}