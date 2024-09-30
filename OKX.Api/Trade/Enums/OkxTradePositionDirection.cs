namespace OKX.Api.Trade;

/// <summary>
/// OKX Position Direction
/// </summary>
public enum OkxTradePositionDirection
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
}