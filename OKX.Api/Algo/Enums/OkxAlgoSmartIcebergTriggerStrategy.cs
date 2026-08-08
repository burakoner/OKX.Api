namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg trigger strategy.
/// </summary>
public enum OkxAlgoSmartIcebergTriggerStrategy : byte
{
    /// <summary>
    /// Trigger immediately.
    /// </summary>
    [Map("instant")]
    Instant = 1,

    /// <summary>
    /// Trigger by price.
    /// </summary>
    [Map("price")]
    Price = 2,

    /// <summary>
    /// Trigger by RSI indicator.
    /// </summary>
    [Map("rsi")]
    RSI = 3,
}
