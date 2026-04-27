namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Trigger Strategy
/// </summary>
public enum OkxDcaTriggerStrategy : byte
{
    /// <summary>
    /// Instant trigger
    /// </summary>
    [Map("instant")]
    Instant = 1,

    /// <summary>
    /// Price trigger
    /// </summary>
    [Map("price")]
    Price = 2,

    /// <summary>
    /// RSI indicator trigger
    /// </summary>
    [Map("rsi")]
    RSI = 3,

    /// <summary>
    /// WebSocket signal trigger
    /// </summary>
    [Map("webhook")]
    Webhook = 4,
}
