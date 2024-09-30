namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Trigger Strategy
/// </summary>
public enum OkxGridTriggerStrategy
{
    /// <summary>
    /// Instant
    /// </summary>
    [Map("instant")]
    Instant = 1,

    /// <summary>
    /// Price
    /// </summary>
    [Map("price")]
    Price = 2,

    /// <summary>
    /// RSI
    /// </summary>
    [Map("rsi")]
    RSI = 3
}