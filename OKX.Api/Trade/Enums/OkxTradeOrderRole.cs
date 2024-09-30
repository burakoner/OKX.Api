namespace OKX.Api.Trade;

/// <summary>
/// OKX Trade Role
/// </summary>
public enum OkxTradeOrderRole
{
    /// <summary>
    /// Taker
    /// </summary>
    [Map("T")]
    Taker,

    /// <summary>
    /// Maker
    /// </summary>
    [Map("M")]
    Maker,
}