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
    Instant,

    /// <summary>
    /// Price
    /// </summary>
    [Map("price")]
    Price,

    /// <summary>
    /// RSI
    /// </summary>
    [Map("rsi")]
    RSI
}